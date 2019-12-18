#!/bin/bash
echo "Trying to update database with migrations"

dotnet ef database update --project ".\Ukiyo.Infrastructure\Ukiyo.Infrastructure.csproj" --startup-project ".\Ukiyo.Api\Ukiyo.Api.csproj" --context "AppDbContext"