using System.IO;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FolderTool.Rendering.Icons;

public static class Icon
{
    private const string fileIconsResourceLocation = "FolderTool.Resources.FileIcons.yaml";
    private const string directoryIconsResourceLocation = "FolderTool.Resources.DirectoryIcons.yaml";

    private static FileIconDefinitionsModel? fileIconDefinitions = null;
    private static DirectoryIconDefinitionsModel? directoryIconDefinitions = null;

    private static readonly IDeserializer deserializer = new DeserializerBuilder()
        .WithNamingConvention(HyphenatedNamingConvention.Instance)
        .Build();



    public static string GetFileIcon(FileInfo file)
    {
        var icons = GetFileIconDefinitions();

        string extension = string.IsNullOrEmpty(file.Extension)
            ? ""
            : file.Extension[1..];
        return icons.Icons
            .FirstOrDefault(icon => icon.GetExtensions().Contains(extension))
            ?.Icon
            ?? icons.Default;
    }

    public static string GetDirectoryIcon(DirectoryInfo directory)
    {
        var icons = GetDirectoryIconDefinitions();

        string name = directory.Name;
        return icons.Icons
            .FirstOrDefault(icon => icon.GetNames().Contains(name))
            ?.Icon
            ?? icons.Default;
    }

    private static FileIconDefinitionsModel GetFileIconDefinitions()
    {
        if (fileIconDefinitions is not null)
        {
            return fileIconDefinitions;
        }

        string resource = ManifestResourceHelper.ReadResource(fileIconsResourceLocation);
        var icons = deserializer.Deserialize<FileIconDefinitionsModel>(resource);

        fileIconDefinitions = icons;
        return icons;
    }

    private static DirectoryIconDefinitionsModel GetDirectoryIconDefinitions()
    {
        if (directoryIconDefinitions is not null)
        {
            return directoryIconDefinitions;
        }

        string resource = ManifestResourceHelper.ReadResource(directoryIconsResourceLocation);
        var icons = deserializer.Deserialize<DirectoryIconDefinitionsModel>(resource);

        directoryIconDefinitions = icons;
        return icons;
    }
}
