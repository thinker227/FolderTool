using System.Collections.Generic;

namespace FolderTool.Rendering.Icons;

public sealed record class DirectoryIcon(
    string Icon,
    IEnumerable<string> Names);
