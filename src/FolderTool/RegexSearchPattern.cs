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
    private readonly bool showHidden;

    private RegexSearchPattern(Regex regex, bool recursive, int maxDepth, bool showHidden)
    {
        this.regex = regex;
        this.recursive = recursive;
        this.maxDepth = maxDepth;
        this.showHidden = showHidden;
    }

    public static RegexSearchPattern Create(string pattern, bool recursive, int maxDepth, bool showHidden)
    {
        var options = RegexOptions.Compiled;
        Regex regex = new(pattern, options);

        return new(regex, recursive, maxDepth, showHidden);
    }

    public IEnumerable<FileSystemInfo> GetEntries(DirectoryInfo rootDirectory)
    {
        var skip
            = FileAttributes.System
            | FileAttributes.Temporary
            ;

        if (!showHidden) skip |= FileAttributes.Hidden;

        EnumerationOptions options = new()
        {
            IgnoreInaccessible = true,
            RecurseSubdirectories = recursive,
            MaxRecursionDepth = maxDepth,
            ReturnSpecialDirectories = false,
            AttributesToSkip = skip,
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
