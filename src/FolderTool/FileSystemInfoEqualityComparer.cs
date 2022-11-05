using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace FolderTool;

internal sealed class FileSystemInfoEqualityComparer : IEqualityComparer<FileSystemInfo?>
{
    public static FileSystemInfoEqualityComparer Instance { get; } = new();

    private FileSystemInfoEqualityComparer() { }

    [DebuggerStepThrough]
    public bool Equals(FileSystemInfo? x, FileSystemInfo? y)
    {
        if (x is null || y is null) return x == y;

        return x.FullName == y.FullName && x.GetType() == y.GetType();
    }

    [DebuggerStepThrough]
    public int GetHashCode([DisallowNull] FileSystemInfo obj) =>
        HashCode.Combine(obj.GetType(), obj.FullName);
}
