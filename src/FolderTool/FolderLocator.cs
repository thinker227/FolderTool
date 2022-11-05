using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderTool;

public static class FolderLocator
{
    public static DisplayFolder GetFolder(DirectoryInfo rootDirectory, ISearchPattern searchPattern)
    {
        var comparer = FileSystemInfoEqualityComparer.Instance;
        var dirComparer = (IEqualityComparer<DirectoryInfo?>)comparer;

        var entries = searchPattern.GetEntries(rootDirectory)
            .ToHashSet(comparer);

        entries.AddRange(entries
            .SelectMany(entry => entry.GetDirectoryHierarchy(rootDirectory))
            .ToArray());

        var directories = entries
            .GroupBy(
                entry => entry.GetParent(),
                comparer: dirComparer)
            .Where(entry => entry.Key?.FullName != rootDirectory.Parent?.FullName)
            .ToDictionary(
                entry => entry.Key!,
                entry => entry.ToArray(),
                comparer: dirComparer);

        return ConstructFolder(directories, rootDirectory);

        //return new(rootDirectory, Enumerable.Empty<DisplayFolder>(), Enumerable.Empty<FileInfo>());
    }

    private static DisplayFolder ConstructFolder(IReadOnlyDictionary<DirectoryInfo, FileSystemInfo[]> directories, DirectoryInfo directory)
    {
        var entries = directories.GetValueOrDefault(directory)
            ?? Enumerable.Empty<FileSystemInfo>();

        var files = entries
            .OfType<FileInfo>()
            .ToArray();

        var subFolders = entries
            .OfType<DirectoryInfo>()
            .Select(subDir => ConstructFolder(directories, subDir))
            .ToArray();

        return new(directory, subFolders, files);
    }
}
