using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace FolderTool;

public sealed class RegexSearchPattern : ISearchPattern
{
    private readonly Regex regex;
    private readonly bool recursive;
    private readonly int maxDepth;

    private RegexSearchPattern(Regex regex, bool recursive, int maxDepth)
    {
        this.regex = regex;
        this.recursive = recursive;
        this.maxDepth = maxDepth;
    }

    public static RegexSearchPattern Create(string pattern, bool recursive, int maxDepth)
    {
        var options = RegexOptions.Compiled;
        Regex regex = new(pattern, options);

        return new(regex, recursive, maxDepth);
    }

    public IEnumerable<FileSystemInfo> GetEntries(DirectoryInfo rootDirectory)
    {
        EnumerationOptions options = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = recursive,
            MaxRecursionDepth = maxDepth,
            ReturnSpecialDirectories = false
        };

        return rootDirectory
            .EnumerateFileSystemInfos("*", options)
            .Where(entry =>
            {
                string path = Path.GetRelativePath(rootDirectory.FullName, entry.FullName);
                return regex.IsMatch(path);
            });
    }
}
