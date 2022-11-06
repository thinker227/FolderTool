using System.Collections.Generic;

namespace FolderTool.Configuration.Models;

internal sealed class DirectoryIconModel
{
    public string Icon { get; init; } = null!;

    public string? Name { get; init; }

    public List<string>? Names { get; init; }
}
