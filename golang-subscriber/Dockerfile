## backend build-phase
## --------------------------------------------------------------------------
FROM golang:alpine AS BACKEND-BUILD

WORKDIR /golang-subscriber
COPY ./go.mod ./
COPY ./go.sum ./
COPY ./main.go ./
RUN GOOS=linux GOARCH=amd64 go build -ldflags="-w -s" -o golang-subscriber ./*.go
## --------------------------------------------------------------------------

## runtime
## --------------------------------------------------------------------------
FROM alpine:latest
LABEL author="Henrik Binggl"
LABEL description="simple example of a pub-sub golang component for a dapr demo"

WORKDIR /opt/golang-subscriber

RUN mkdir -p /opt/golang-subscriber/
COPY --from=BACKEND-BUILD /golang-subscriber/golang-subscriber /opt/golang-subscriber

EXPOSE 3000

# Do not run as root user
## alpine specific user/group creation
RUN addgroup -S golang-subscriber && \
    adduser -S -D -H -u 1000 -G golang-subscriber -h /opt/golang-subscriber golang-subscriber

RUN chown -R golang-subscriber:golang-subscriber /opt/golang-subscriber
USER golang-subscriber

CMD ["/opt/golang-subscriber/golang-subscriber"]
