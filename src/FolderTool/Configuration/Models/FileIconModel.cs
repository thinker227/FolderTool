using System.Collections.Generic;

namespace FolderTool.Configuration.Models;

internal sealed class FileIconModel
{
    public string Icon { get; init; } = null!;

    public string? Extension { get; init; }

    public List<string>? Extensions { get; init; }
}
