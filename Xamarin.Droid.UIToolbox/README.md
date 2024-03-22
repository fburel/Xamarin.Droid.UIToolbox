# Toolbox For Xamarin Droid

## Purpose

## Usage

## Classes

### Activity Navigation

### Displaying lists

### Input forms

### Others

## publish a new version of the packages

### update the .nuspec file (version number, dependencies, ...)

### build toolbox in production mode

### pack and upload

```
nuget pack toolbox4droid.nuspec  
nuget push *.nupkg -Source https://api.nuget.org/v3/index.json
rm -rf *.nupkg
```