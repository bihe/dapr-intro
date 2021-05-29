#vars
REPO=bihe
IMAGE_REACT=${REPO}/dapr-react-form:latest

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
		npm run buildclient
	    npm install

dapr-run: build
		@dapr run --app-id react-form --app-port 8080 npm run start

docker-build:
		@docker build --pull -t ${IMAGE_REACT} ./

docker-run: docker-build
		@docker run -it -p 1127.0.0.1:8080:8080 ${IMAGE_REACT}

clean:
		rm -rf ./node_modules
		rm -rf ./client/node_modules
		docker rmi -f ${IMAGE_REACT}

all: build