using System.Collections.Generic;

namespace FolderTool.Rendering.Icons;

public sealed record class DirectoryIconDefinitions(
    string DefaultIcon,
    IEnumerable<DirectoryIcon> Icons);
