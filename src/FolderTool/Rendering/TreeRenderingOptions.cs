using FolderTool.Rendering.Icons;

namespace FolderTool.Rendering;

public struct TreeRenderingOptions
{
    public PathDisplayMode PathDisplayMode { get; set; }

    public bool Fancy { get; set; }

    public FileIconDefinitions FileIcons { get; set; }

    public DirectoryIconDefinitions DirectoryIcons { get; set; }
}
