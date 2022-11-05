using System.Collections.Generic;

namespace FolderTool.Rendering.Icons;

public sealed class FileIconDefinitionsModel
{
    public string Default { get; set; } = "?";

    public List<FileIconModel> Icons { get; set; } = new();
}
