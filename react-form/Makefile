#vars
REPO=bihe
IMAGE_REACT=${REPO}/dapr-react-form:latest

.PHONY: help build dapr-run docker-build docker-run clean all

# some windows magic
ifeq ($(OS),Windows_NT)
SHELL := pwsh.exe
.SHELLFLAGS := -NoProfile -Command
endif

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
	npm run buildclient
	npm install

dapr-run: build
	@dapr run --app-id react-form --app-port 8080 npm run start

docker-build:
	@docker build --pull -t ${IMAGE_REACT} ./

docker-run: docker-build
	@docker run -it -p 1127.0.0.1:8080:8080 ${IMAGE_REACT}

clean:
ifeq ($(OS),Windows_NT)
	Remove-Item -Force -Recurse ./node_modules
	Remove-Item -Force -Recurse ./client/node_modules
	docker rmi -f ${IMAGE_REACT}
else
	rm -rf ./node_modules
	rm -rf ./client/node_modules
	docker rmi -f ${IMAGE_REACT}
endif


all: build
