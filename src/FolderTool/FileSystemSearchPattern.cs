using System.Collections.Generic;
using System.IO;

namespace FolderTool;

public sealed class FileSystemSearchPattern : ISearchPattern
{
    private readonly string pattern;
    private readonly bool recursive;
    private readonly int maxDepth;

    public FileSystemSearchPattern(string pattern, bool recursive, int maxDepth)
    {
        this.pattern = pattern;
        this.recursive = recursive;
        this.maxDepth = maxDepth;
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

        return rootDirectory.EnumerateFileSystemInfos(pattern, options);
    }
}
