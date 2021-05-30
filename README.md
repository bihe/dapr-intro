# dapr pub-sub example
This repo is just a copy of the original [dapr](https://github.com/dapr/dapr) pubsub quickstart [sample](https://github.com/dapr/quickstarts/tree/master/pub-sub). 

The only changes are, that a [golang](https://golang.org/) and a [.NET](https://dotnet.microsoft.com/) subscriber are used (instead of the "original" nodejs, pyhton ones) and that [redis](https://redis.io/topics/streams-intro) can be swapped out with [Azure Service Bus](https://azure.microsoft.com/en-us/services/service-bus/).

**TL;DR;**

In a nutshell a **react form** publishes a message via an **nodejs/express** app to a pubsub component. Two subscribers one **golang** one **dotnet** react based on topics to the messages received through the subscription.

![darp](./doc/pubsub_overview.png)

# Setup
For the examples to work, the following software components need to be installed:

- dapr (1.x) - https://docs.dapr.io/getting-started/install-dapr-cli/
- golang (1.16.x) - https://golang.org/dl/
- dotnet (5.x) - https://dotnet.microsoft.com/download
- nodejs (14.x) - https://nodejs.org/en/download/
- docker (20.x) - https://docs.docker.com/get-docker/
- mininkube, kubectl (1.20.x) - https://kubernetes.io/docs/tasks/tools/

For k8s/minikube follow these steps to enalbe [dapr for k8s](https://github.com/dapr/quickstarts/tree/master/hello-kubernetes) including the deployment of [redis](https://docs.dapr.io/getting-started/configure-state-pubsub/#create-a-redis-store) as a store and pubsub component.

# dapr CLI
Each of the 3 components (react-form, golang-subscriber, dotnet-subscriber) has a Makefile which can be used to compile the apps and run them via dapr cli.

e.g. dotnet-subscriber

```
make dapr-run
```

Open seperate terminals and change to the respectice directories and start the app via the dapper cli.

**HINT**:

If on windows without proper Makefile support just execute the command manually:

```
dapr run --app-id dotnet-subscriber --app-port 5000 ./output/dotnet-subscriber
```

![dapr cli](./doc/dapr-pubsub-cli.png)

Open the react-form in a browser and send messages to the different topics and see the result in the output of the applications.

# Kubernetes
The kubernetes approach works almost the same, but of-course it uses containers to run the apps. For this purpose each application has a **Dockerfile** which produces the required container images. Dapr injects the sidecars via the dapr operator and enables the communication between the components.

The **./deploy** folder contains a Makefile which can be used to deploy/undeploy the apps and components:

```
make deploy-redis
```

uses ```kubectl``` to deploy the applications and components defined in the yaml files. 

![dapr k8s](./doc/dapr-pubsub-k8s-redis.png)

## Local images
The application images are not published to dockerhub or any other container-registry, because this would just be waste for this demo-purpouse. Instead the images are held locally and k8s is instructed to **not pull** the images!

To build locally a Makefile is available:

```
make build
```
creates the local images.


```yaml
containers:
      - name: golang-subscriber
        image: bihe/dapr-golang-subscriber:latest
        ports:
        - containerPort: 3000
        imagePullPolicy: Never
```

The important part is the **imagePullPolicy**. To actually enable this, it is not sufficient to just build the images locally, because minikube does not access those local images. As usual there is an [StackOverflow answer](https://stackoverflow.com/questions/56392041/getting-errimageneverpull-in-pods) how to give the images to minikube!

The trick is to set some relevant ENVs via minikube and build again:

```
eval $(minikube docker-env)
```

## PubSub components
dapr provides abstraction, so from an application-perspective it is not necessary to interact with the components directly, but only via the dapr API. To enable this, the components need to be declared.

The following yaml defines a redis-powered PubSub component, where a k8s deployed redis is used.

```yaml
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: pubsub
spec:
  type: pubsub.redis
  version: v1
  metadata:
  - name: redisHost
    value: redis-master:6379
  - name: redisPassword
    secretKeyRef:
      name: redis
      key: redis-password
auth:
  secretStore: kubernetes
```

The nice abstraction feature of dapr enables us, to swap the PubSub component. In this example the redis-based pubsub is replaced with Azure Service-Bus:

```yaml
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: pubsub
spec:
  type: pubsub.azure.servicebus
  version: v1
  metadata:
  - name: connectionString
    secretKeyRef:
      name: az-sb
      key: connstr
auth:
  secretStore: kubernetes
```

Without any change in the application logic we can now use Service-Bus instead of redis!

![make](./doc/dapr-pubsub-k8s-az-sb.png)

# Links

- Getting-Started: https://docs.dapr.io/getting-started/
- PubSub-Quickstart: https://github.com/dapr/quickstarts/tree/master/pub-sub
- PubSub-Overview: https://docs.dapr.io/developing-applications/building-blocks/pubsub/pubsub-overview/
- Azure-Service-Bus PubSusb: https://docs.dapr.io/reference/components-reference/supported-pubsub/setup-azure-servicebus/
- PubSub with Azure Service Bus: https://www.youtube.com/watch?v=umrUlfrZqKk


