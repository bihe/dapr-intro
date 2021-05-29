#vars

.PHONY: help deploy-redis undeploy-redis deploy-azsb undeploy-azsb

help:
	@echo "Makefile arguments:"
	@echo ""
	@echo "Makefile commands:"
	@echo "	- deploy-redis"
	@echo "	- undeploy-redis"
	@echo "	- deploy-azsb"
	@echo "	- undeploy-azsb"
	@echo "	- help"

.DEFAULT_GOAL := help

deploy-redis: _deploy-app
	@kubectl apply -f ./redis/pubsub.yaml

undeploy-redis: _undeploy-app
	@kubectl delete -f ./redis/pubsub.yaml

deploy-azsb: _deploy-app
	@kubectl apply -f ./az-sb/pubsub.yaml

undeploy-azsb: _undeploy-app
	@kubectl delete -f ./az-sb/pubsub.yaml

_deploy-app:
	@kubectl apply -f ./dotnet-subscriber.yaml
	@kubectl apply -f ./golang-subscriber.yaml
	@kubectl apply -f ./react-form.yaml

_undeploy-app:
	@kubectl delete -f ./dotnet-subscriber.yaml
	@kubectl delete -f ./golang-subscriber.yaml
	@kubectl delete -f ./react-form.yaml

