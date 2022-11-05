using System.IO;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FolderTool.Rendering.Icons;

public static class Icon
{
    private const string fileIconsResourceLocation = "FolderTool.Resources.FileIcons.yaml";
    private const string directoryIconsResourceLocation = "FolderTool.Resources.DirectoryIcons.yaml";

    private static readonly IDeserializer deserializer = new DeserializerBuilder()
        .WithNamingConvention(HyphenatedNamingConvention.Instance)
        .Build();

    public static string GetFileIcon(FileInfo file)
    {
        string resource = ManifestResourceHelper.ReadResource(fileIconsResourceLocation);
        var icons = deserializer.Deserialize<FileIconDefinitionsModel>(resource);

        string extension = file.Extension[1..];
        return icons.Icons
            .FirstOrDefault(icon => icon.GetExtensions().Contains(extension))
            ?.Icon
            ?? icons.Default;
    }

    public static string GetDirectoryIcon(DirectoryInfo directory)
    {
        string resource = ManifestResourceHelper.ReadResource(directoryIconsResourceLocation);
        var icons = deserializer.Deserialize<DirectoryIconDefinitionsModel>(resource);

        string name = directory.Name;
        return icons.Icons
            .FirstOrDefault(icon => icon.GetNames().Contains(name))
            ?.Icon
            ?? icons.Default;
    }
}
