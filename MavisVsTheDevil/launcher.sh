#!/bin/bash
echo "Pulling Latest"
git pull
git lfs fetch --all
git lfs checkout
echo "running"
dotnet run --project ./MavisVsTheDevil.csproj