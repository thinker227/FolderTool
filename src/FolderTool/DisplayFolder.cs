using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderTool;

public readonly record struct DisplayFolder(
    DirectoryInfo Directory,
    IEnumerable<DisplayFolder> SubFolders,
    IEnumerable<FileInfo> Files)
{
    public bool Empty =>
        !SubFolders.Any() && !Files.Any();

    public bool Equals(DisplayFolder other) =>
        Directory.FullName == other.Directory.FullName;

    public override int GetHashCode() =>
        Directory.FullName.GetHashCode();

    public override string ToString() =>
        Directory.FullName;
}
