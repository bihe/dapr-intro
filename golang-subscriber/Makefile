#vars
REPO=bihe
IMAGE_GOLANG=${REPO}/dapr-golang-subscriber:latest

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
ifeq ($(OS),Windows_NT)
	go build -ldflags="-w -s" -o golang-subscriber.exe ./...
else
	GOOS=linux GOARCH=amd64 go build -ldflags="-w -s" -o golang-subscriber ./...
endif

dapr-run: build
	@dapr run --app-id golang-subscriber --app-port 3000 ./golang-subscriber

docker-build:
	@docker build --pull -t ${IMAGE_GOLANG} ./

docker-run: docker-build
	@docker run -it -p 127.0.0.1:3000:3000 ${IMAGE_GOLANG}

clean:
ifeq ($(OS),Windows_NT)
	pwsh -noprofile -command 'Remove-Item -Force -Recurse ./golang-subscriber.exe'
	docker rmi -f ${IMAGE_DOTNET}
else
	rm -f ./golang-subscriber
	docker rmi -f ${IMAGE_DOTNET}
endif

all: build
