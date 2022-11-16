using System.Collections.Generic;
using System.IO;

namespace FolderTool;

public sealed class FileSystemSearchPattern : ISearchPattern
{
    private readonly string pattern;
    private readonly bool recursive;
    private readonly int maxDepth;
    private readonly bool showHidden;

    public FileSystemSearchPattern(string pattern, bool recursive, int maxDepth, bool showHidden)
    {
        this.pattern = pattern;
        this.recursive = recursive;
        this.maxDepth = maxDepth;
        this.showHidden = showHidden;
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

        return rootDirectory.EnumerateFileSystemInfos(pattern, options);
    }
}
