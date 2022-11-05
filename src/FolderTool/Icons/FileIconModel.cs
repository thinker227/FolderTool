using System.Collections.Generic;
using System.Linq;

namespace FolderTool.Rendering.Icons;

public sealed class FileIconModel
{
    public string Icon { get; set; } = null!;

    public string? Extension { get; set; }

    public List<string>? Extensions { get; set; }



    public IEnumerable<string> GetExtensions() => (Extension, Extensions) switch
    {
        (not null, null) => new[] { Extension },
        (null, not null) => Extensions,
        (not null, not null) => Extensions.Append(Extension),
        _ => Enumerable.Empty<string>()
    };
}
