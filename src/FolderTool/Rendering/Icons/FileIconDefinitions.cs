using System.Collections.Generic;

namespace FolderTool.Rendering.Icons;

public sealed record class FileIconDefinitions(
    string DefaultIcon,
    IEnumerable<FileIcon> Icons);
