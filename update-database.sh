#!/bin/bash
echo "Trying to update database with migrations"

dotnet ef database update --project ".\Ukiyo.Core\Ukiyo.Core.csproj" --startup-project ".\Ukiyo.Api\Ukiyo.Api.csproj" --context "AppDbContext"