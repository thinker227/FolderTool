using System.Collections.Generic;

namespace FolderTool.Rendering.Icons;

public sealed record class FileIcon(
    string Icon,
    IEnumerable<string> Extensions);
