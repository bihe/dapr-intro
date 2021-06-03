#vars
REPO=bihe
IMAGE_GOLANG=${REPO}/dapr-golang-subscriber:latest
IMAGE_DOTNET=${REPO}/dapr-dotnet-subscriber:latest
IMAGE_REACT=${REPO}/dapr-react-form:latest

.PHONY: help build all

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
	@echo "all"

.DEFAULT_GOAL := all

build:
	@docker build --pull -t ${IMAGE_GOLANG} ./golang-subscriber/
	@docker build --pull -t ${IMAGE_DOTNET} ./dotnet-subscriber/
	@docker build --pull -t ${IMAGE_REACT} ./react-form/

all: build
