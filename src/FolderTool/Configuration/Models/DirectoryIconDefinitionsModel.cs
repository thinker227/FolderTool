using System.Collections.Generic;

namespace FolderTool.Configuration.Models;

internal sealed class DirectoryIconDefinitionsModel
{
    public string Default { get; init; } = null!;

    public List<DirectoryIconModel> Icons { get; init; } = new();
}
