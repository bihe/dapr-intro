#vars

.PHONY: help deploy-redis undeploy-redis deploy-azsb undeploy-azsb deploy-app-arm64 undeploy-app-arm64

help:
	@echo "Makefile arguments:"
	@echo ""
	@echo "Makefile commands:"
	@echo "	- deploy-redis"
	@echo "	- undeploy-redis"
	@echo "	- deploy-azsb"
	@echo "	- undeploy-azsb"
	@echo "	- deploy-app-arm64"
	@echo "	- undeploy-app-arm64"
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
	@kubectl apply -f ./react-form.yaml
	@kubectl apply -f ./dotnet-subscriber.yaml
	@kubectl apply -f ./golang-subscriber.yaml
	@kubectl apply -f ./golang-subscriber-subscribe-ALL.yaml
	@kubectl apply -f ./golang-subscriber-subscribe-Topic1.yaml

_undeploy-app:
	@kubectl delete -f ./react-form.yaml
	@kubectl delete -f ./dotnet-subscriber.yaml
	@kubectl delete -f ./golang-subscriber.yaml
	@kubectl delete -f ./golang-subscriber-subscribe-ALL.yaml
	@kubectl delete -f ./golang-subscriber-subscribe-Topic1.yaml

deploy-app-arm64:
	@kubectl apply -f ./react-form-arm64.yaml
	@kubectl apply -f ./dotnet-subscriber-arm64.yaml
	@kubectl apply -f ./golang-subscriber-arm64.yaml
	@kubectl apply -f ./golang-subscriber-subscribe-ALL.yaml
	@kubectl apply -f ./golang-subscriber-subscribe-Topic1.yaml

undeploy-app-arm64:
	@kubectl delete -f ./react-form-arm64.yaml
	@kubectl delete -f ./dotnet-subscriber-arm64.yaml
	@kubectl delete -f ./golang-subscriber-arm64.yaml
	@kubectl delete -f ./golang-subscriber-subscribe-ALL.yaml
	@kubectl delete -f ./golang-subscriber-subscribe-Topic1.yaml
