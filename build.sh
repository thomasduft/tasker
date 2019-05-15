#!/bin/sh

if [ -z "$1" ]
then
  echo No version specified! Please specify a valid version like 1.2.3!
  exit 1
else
  echo version $1
fi

echo Restore solution
dotnet restore microcron.sln

echo Packaging solution
dotnet pack src/tasker.AspNetCoreEngine/ -c Release /p:PackageVersion=$1 /p:Version=$1 -o ./../../dist/nupkgs

echo Done
