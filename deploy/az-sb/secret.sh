#!/bin/sh
kubectl delete secret az-sb
kubectl create secret generic az-sb --from-file=connstr=./connectionstring.txt