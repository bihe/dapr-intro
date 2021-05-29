## build
## --------------------------------------------------------------------------
FROM node:current-alpine AS BUILD
WORKDIR /usr/src/app
COPY . .

RUN npm run buildclient
RUN npm install

## runtime
## --------------------------------------------------------------------------
FROM node:current-alpine AS RUNTIME
LABEL author="Henrik Binggl"
LABEL description="simple example of a pub-sub react-form for a dapr demo"

WORKDIR /opt/react-form

COPY --from=BUILD /usr/src/app/client /opt/react-form/client/
COPY --from=BUILD /usr/src/app/node_modules /opt/react-form/node_modules/
COPY --from=BUILD /usr/src/app/server.js /opt/react-form/
COPY --from=BUILD /usr/src/app/package.json /opt/react-form/
COPY --from=BUILD /usr/src/app/package-lock.json /opt/react-form/

RUN npm install -g nodemon

EXPOSE 8080

# Do not run as root user
## alpine specific user/group creation
RUN addgroup -S react-form && \
    adduser -S -D -H -G react-form -h /opt/react-form react-form

RUN chown -R react-form:react-form /opt/react-form
USER react-form

CMD [ "npm", "run", "server" ]