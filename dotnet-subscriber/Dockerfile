## --------------------------------------------------------------------------
## backend build-phase
## --------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS BACKEND-BUILD
WORKDIR /dotnet-subscriber
COPY ./*.csproj ./
COPY ./*.cs ./
COPY ./appsettings.json ./
COPY ./Controllers/ ./
RUN dotnet build -c Release dotnet-subscriber.csproj  && dotnet publish -c Release -v m --self-contained true --runtime linux-musl-x64 -o output  dotnet-subscriber.csproj 

## --------------------------------------------------------------------------
## runtime build
## --------------------------------------------------------------------------
FROM alpine:latest
LABEL author="Henrik Binggl"
LABEL description="simple example of a pub-sub dotnet component for a dapr demo"

RUN apk add --no-cache \ 
    openssh \
    zlib \
    libstdc++ \
    icu

WORKDIR /opt/dotnet-subscriber
RUN mkdir -p /opt/dotnet-subscriber
COPY --from=BACKEND-BUILD /dotnet-subscriber/output/* /opt/dotnet-subscriber/

EXPOSE 5000
ENV ASPNETCORE_ENVIRONMENT Production
ENV ASPNETCORE_URLS http://*:5000

# Do not run as root user
RUN addgroup -S dotnet-subscriber && \
    adduser -S -D -H -u 1000 -G dotnet-subscriber -h /opt/dotnet-subscriber dotnet-subscriber

RUN chown -R dotnet-subscriber:dotnet-subscriber /opt/dotnet-subscriber
USER dotnet-subscriber
CMD ["/opt/dotnet-subscriber/dotnet-subscriber"]
