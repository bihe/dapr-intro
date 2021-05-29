#vars
REPO=bihe
IMAGE_DOTNET=${REPO}/dapr-dotnet-subscriber:latest

.PHONY: help build dapr-run docker-build docker-run clean all

help:
	@echo "Makefile arguments:"
	@echo ""
	@echo "Makefile commands:"
	@echo "build"
	@echo "dapr-run"
	@echo "docker-build"
	@echo "docker-run"
	@echo "clean"
	@echo "all"

.DEFAULT_GOAL := all

build:
	dotnet clean -c Release && dotnet build -c Release && dotnet publish -c Release -v m --self-contained true --runtime linux-x64 -o output  dotnet-subscriber.csproj 

dapr-run: build
	@dapr run --app-id dotnet-subscriber --app-port 5000 ./output/dotnet-subscriber

docker-build:
	@docker build --pull -t ${IMAGE_DOTNET} ./

docker-run: docker-build
	@docker run -it -p 127.0.0.1:5000:5000 ${IMAGE_DOTNET}

clean:
	rm -fr ./output
	docker rmi -f ${IMAGE_DOTNET}

all: build
