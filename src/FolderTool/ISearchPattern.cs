using System.Collections.Generic;
using System.IO;

namespace FolderTool;

public interface ISearchPattern
{
    IEnumerable<FileSystemInfo> GetEntries(DirectoryInfo rootDirectory);
}
