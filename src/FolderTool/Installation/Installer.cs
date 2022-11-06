﻿using System.IO;
using Spectre.Console;

namespace FolderTool.Installation;

public sealed class Installer
{
    public static void Install(DirectoryInfo currentDirectory)
    {
        const string pathEnvVarName = EnvironmentPath.pathEnvVarName;
        string dirPath = currentDirectory.FullName;

        var pathResult = EnvironmentPath.AddToPath(dirPath);

        switch (pathResult)
        {
            case EnvironmentPath.AddToPathResult.Success:
                AnsiConsole.MarkupLine($"[lime]Added '{dirPath}' to {pathEnvVarName}.[/]");
                break;

            case EnvironmentPath.AddToPathResult.AlreadyPresent:
                AnsiConsole.MarkupLine($"[lime]'{dirPath}' was already present on {pathEnvVarName}.[/]");
                break;

            case EnvironmentPath.AddToPathResult.MissingAccess:
                AnsiConsole.MarkupLine($"[red]Failed to set {pathEnvVarName} because the process lacks access. Try running the installer as an administrator.[/]");
                break;
        }
    }
}