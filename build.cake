#addin nuget:?package=Cake.Git&version=0.21.0
#addin nuget:?package=Nuget.Core&version=2.14.0

using NuGet;

var target = Argument("target", "Default");
var solutionPath = "./Toolbox.Droid.sln";
var project = "./Toolbox.Droid/Toolbox.Droid.csproj";
var configuration = "Release";

var buildDir = Directory("./Toolbox.Droid/bin") + Directory(configuration);
var objDir = Directory("./Toolbox.Droid/obj") + Directory(configuration);


Task("Clean")
    .Does(() => {
        Information("Cleaning...");
        if (DirectoryExists(buildDir))
        {
            DeleteDirectory(
                buildDir, 
                new DeleteDirectorySettings {
                    Recursive = true,
                    Force = true
                }
            );
        }

        if (DirectoryExists(objDir))
        {
            DeleteDirectory(
                objDir, 
                new DeleteDirectorySettings {
                    Recursive = true,
                    Force = true
                }
            );
        }
       
        DotNetCoreClean(solutionPath);
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
{
    Information("Restoring nugget packages...");
    DotNetCoreRestore(solutionPath);
});

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
        Information("Building...");
        DotNetCoreBuild(
            solutionPath,
            new DotNetCoreBuildSettings 
            {
                Configuration = configuration
            }
        );
    });

Task("Package")
    .IsDependentOn("Build")
    .Does(() => {
        Information("Packaging...");
        var settings = new DotNetCorePackSettings
        {
            OutputDirectory = buildDir,
            NoBuild = true
        };
        DotNetCorePack(project, settings);
    });

Task("Default")
    .IsDependentOn("Package");

RunTarget(target);
