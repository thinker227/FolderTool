using System.Collections.Generic;

namespace FolderTool.Configuration.Models;

internal sealed class FileIconDefinitionsModel
{
    public string Default { get; init; } = null!;

    public List<FileIconModel> Icons { get; init; } = new();
}
