using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderTool;

internal static class Extensions
{
    public static DirectoryInfo? GetParent(this FileSystemInfo info) =>
        Directory.GetParent(info.FullName);

    public static IEnumerable<DirectoryInfo> GetDirectoryHierarchy(this FileSystemInfo info, DirectoryInfo? root = null)
    {
        var empty = Enumerable.Empty<DirectoryInfo>();

        if (info is DirectoryInfo && info.FullName == root?.FullName) return empty;

        var parent = info.GetParent();
        if (parent is null) return empty;

        return parent.GetDirectoryHierarchy(root).Prepend(parent);
    }

    public static void AddRange<T>(this ISet<T> set, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            set.Add(item);
        }
    }
}
