using System.Collections.Generic;

namespace FolderTool.Rendering.Icons;

public sealed class DirectoryIconDefinitionsModel
{
    public string Default { get; set; } = "?";

    public List<DirectoryIconModel> Icons { get; set; } = new();
}
