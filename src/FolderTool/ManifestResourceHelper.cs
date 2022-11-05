using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace FolderTool;

internal static class ManifestResourceHelper
{
    private static readonly Dictionary<string, string> cache = new();

    public static string ReadResource(string resourceLocation)
    {
        if (cache.TryGetValue(resourceLocation, out string? cached))
        {
            return cached;
        }

        var assembly = Assembly.GetExecutingAssembly();

        using var resourceStream = assembly.GetManifestResourceStream(resourceLocation);
        if (resourceStream is null)
        {
            throw new IOException($"Could not read manifest resource '{resourceLocation}' in assembly '{assembly.FullName}'.");
        }

        StreamReader reader = new(resourceStream);
        string content = reader.ReadToEnd();
        cache.Add(resourceLocation, content);

        return content;
    }
}
