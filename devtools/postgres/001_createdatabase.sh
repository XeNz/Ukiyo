#!/usr/bin/env bash
createdb -U postgres ukiyo_test

# Run all migration scripts, just like in a clean environment
for f in /deploy/migration/*; do
    echo "$0: running $f"; psql -U postgres -U postgres -d ukiyo_test -f "$f";
    echo
done
#
# Optionally run scripts with seed data
for f in /docker-entrypoint-initdb.d/seed/*; do
    echo "$0: running $f"; psql -U postgres -U postgres -d ukiyo_test -f "$f";
    echo
done
