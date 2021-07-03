#vars
REPO=bihe
IMAGE_DOTNET=${REPO}/dapr-dotnet-subscriber:latest

.PHONY: help build dapr-run docker-build docker-run clean all docker-build-arm64

help:
	@echo "Makefile arguments:"
	@echo ""
	@echo "Makefile commands:"
	@echo "build"
	@echo "dapr-run"
	@echo "docker-build"
	@echo "docker-build-arm64"
	@echo "docker-run"
	@echo "clean"
	@echo "all"

.DEFAULT_GOAL := all

build:
ifeq ($(OS),Windows_NT)
	dotnet clean -c Release && dotnet build -c Release && dotnet publish -c Release -v m --self-contained true --runtime win-x64 -o output  dotnet-subscriber.csproj
else
	dotnet clean -c Release && dotnet build -c Release && dotnet publish -c Release -v m --self-contained true --runtime linux-x64 -o output  dotnet-subscriber.csproj
endif

dapr-run: build
ifeq ($(OS),Windows_NT)
	@dapr run --app-id dotnet-subscriber --app-port 5000 ./output/dotnet-subscriber.exe
else
	@dapr run --app-id dotnet-subscriber --app-port 5000 ./output/dotnet-subscriber
endif


docker-build:
	@docker build --pull -t ${IMAGE_DOTNET} ./

docker-build-arm64:
	@docker build --pull -t 192.168.1.111:32000/${IMAGE_DOTNET}-arm64 -f Dockerfile.arm64 ./
	@docker push 192.168.1.111:32000/${IMAGE_DOTNET}-arm64

docker-run: docker-build
	@docker run -it -p 127.0.0.1:5000:5000 ${IMAGE_DOTNET}

clean:
ifeq ($(OS),Windows_NT)
	pwsh -noprofile -command 'Remove-Item -Force -Recurse .\output\'
	docker rmi -f ${IMAGE_DOTNET}
else
	rm -fr ./output
	docker rmi -f ${IMAGE_DOTNET}
endif


all: build
