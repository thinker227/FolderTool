using System.Collections.Generic;
using System.Linq;

namespace FolderTool.Rendering.Icons;

public sealed class DirectoryIconModel
{
    public string Icon { get; set; } = null!;

    public string? Name { get; set; }

    public List<string>? Names { get; set; }



    public IEnumerable<string> GetNames() => (Name, Names) switch
    {
        (not null, null) => new[] { Name },
        (null, not null) => Names,
        (not null, not null) => Names.Append(Name),
        _ => Enumerable.Empty<string>()
    };
}
