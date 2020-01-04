#!/bin/bash

for ARGUMENT in "$@"
do

    KEY=$(echo $ARGUMENT | cut -f1 -d=)
    VALUE=$(echo $ARGUMENT | cut -f2 -d=)   

    case "$KEY" in
            -MIGRATIONNAME)              MIGRATIONNAME=${VALUE} ;;
            *)   
    esac    


done

[ -z "$MIGRATIONNAME" ] && echo "Migration name was not entered or valid. Please use the -MIGRATIONNAME argument." && exit 1

echo "Trying to create migration with name $MIGRATIONNAME"

dotnet ef migrations add $MIGRATIONNAME --project ".\Ukiyo.Core\Ukiyo.Core.csproj" --startup-project ".\Ukiyo.Api\Ukiyo.Api.csproj" --context "AppDbContext"