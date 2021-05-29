#vars
REPO=bihe
IMAGE_GOLANG=${REPO}/dapr-golang-subscriber:latest
IMAGE_DOTNET=${REPO}/dapr-dotnet-subscriber:latest
IMAGE_REACT=${REPO}/dapr-react-form:latest

.PHONY: help build push all

help:
	@echo "Makefile arguments:"
	@echo ""
	@echo "Makefile commands:"
	@echo "build"
	@echo "push"
	@echo "all"

.DEFAULT_GOAL := all

build:
	@docker build --pull -t ${IMAGE_GOLANG} ./golang-subscriber/
	@docker build --pull -t ${IMAGE_DOTNET} ./dotnet-subscriber/
	@docker build --pull -t ${IMAGE_REACT} ./react-form/

push:
	@docker push ${IMAGE_GOLANG}
	@docker push ${IMAGE_DOTNET}
	@docker push ${IMAGE_REACT}

all: build
