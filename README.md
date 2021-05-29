# dapr pub-sub example


## kubernetes


# 2 containers in pod (sidecar)
[dotnet-subscriber dapr]
kubectl logs deployment/dotnet-subscriber -c dotnet-subscriber

Minikube and images

imagePullPolicy: Never
eval $(minikube docker-env)
https://stackoverflow.com/questions/56392041/getting-errimageneverpull-in-pods
