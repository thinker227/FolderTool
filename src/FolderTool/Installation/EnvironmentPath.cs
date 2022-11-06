using System.Linq;
using System;

namespace FolderTool.Installation;

internal static class EnvironmentPath
{
    public const string pathEnvVarName =
#if DEBUG
        "NotActualPath";
#else
        "Path";
#endif
    private const EnvironmentVariableTarget variableTarget = EnvironmentVariableTarget.Machine;



    public static string[] GetPath()
    {
        string path = Environment.GetEnvironmentVariable(pathEnvVarName, variableTarget) ?? "";
        return path.Split(';', StringSplitOptions.RemoveEmptyEntries);
    }

    public static AddToPathResult AddToPath(string value)
    {
        string[] pathVars = GetPath();

        if (pathVars.Contains(value))
        {
            return AddToPathResult.AlreadyPresent;
        }

        string path = string.Join(';', pathVars.Append(value));

        try
        {
            Environment.SetEnvironmentVariable(pathEnvVarName, path, variableTarget);
            return AddToPathResult.Success;
        }
        catch (System.Security.SecurityException)
        {
            return AddToPathResult.MissingAccess;
        }
    }

    public enum AddToPathResult
    {
        Success,
        AlreadyPresent,
        MissingAccess
    }
}
