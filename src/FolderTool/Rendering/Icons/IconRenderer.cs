using System.IO;
using System.Linq;

namespace FolderTool.Rendering.Icons;

public sealed class IconRenderer
{
    private readonly FileIconDefinitions fileIcons;
    private readonly DirectoryIconDefinitions directoryIcons;



    public IconRenderer(FileIconDefinitions fileIcons, DirectoryIconDefinitions directoryIcons)
    {
        this.fileIcons = fileIcons;
        this.directoryIcons = directoryIcons;
    }



    public string GetFileIcon(FileInfo file)
    {
        string extension = string.IsNullOrEmpty(file.Extension)
            ? ""
            : file.Extension[1..];
        return fileIcons.Icons
            .FirstOrDefault(icon => icon.Extensions.Contains(extension))
            ?.Icon
            ?? fileIcons.DefaultIcon;
    }

    public string GetDirectoryIcon(DirectoryInfo directory)
    {
        string name = directory.Name;
        return directoryIcons.Icons
            .FirstOrDefault(icon => icon.Names.Contains(name))
            ?.Icon
            ?? directoryIcons.DefaultIcon;
    }
}
