#!/bin/bash
function cleanup() {
  docker-compose -f docker-compose.yml rm -f -s
}
trap cleanup EXIT

set -e

docker-compose -f docker-compose.yml up postgres 