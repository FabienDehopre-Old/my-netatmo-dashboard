#!/usr/bin/env bash

openssl req \
  -newkey rsa:4096 \
  -x509 \
  -nodes \
  -keyout server.key \
  -new \
  -out server.crt \
  -config ./openssl-custom.cnf \
  -sha256 \
  -days 365
