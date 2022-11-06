using System.Collections.Generic;
using System.Linq;
using FolderTool.Configuration.Models;
using FolderTool.Rendering.Icons;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace FolderTool.Configuration;

public static class ConfigurationReader
{
    private static readonly IDeserializer deserializer = new DeserializerBuilder()
        .WithNamingConvention(HyphenatedNamingConvention.Instance)
        .Build();

    public static (FileIconDefinitions, DirectoryIconDefinitions) GetIcons()
    {
        var fileIconsModel = ReadFromManifestResource<FileIconDefinitionsModel>("FolderTool.Resources.FileIcons.yaml");
        FileIconDefinitions fileIcons = new(
            fileIconsModel.Default,
            fileIconsModel.Icons.Select(model => new FileIcon(
                model.Icon,
                GetExtensions(model)))
            .ToArray());

        var directoryIconsModel = ReadFromManifestResource<DirectoryIconDefinitionsModel>("FolderTool.Resources.DirectoryIcons.yaml");
        DirectoryIconDefinitions directoryIcons = new(
            directoryIconsModel.Default,
            directoryIconsModel.Icons.Select(model => new DirectoryIcon(
                model.Icon,
                GetNames(model)))
            .ToArray());

        return (fileIcons, directoryIcons);
    }

    private static IEnumerable<string> GetExtensions(FileIconModel model) => (model.Extension, model.Extensions) switch
    {
        (not null, null) => new[] { model.Extension },
        (null, not null) => model.Extensions,
        (not null, not null) => model.Extensions.Append(model.Extension),
        _ => Enumerable.Empty<string>()
    };

    private static IEnumerable<string> GetNames(DirectoryIconModel model) => (model.Name, model.Names) switch
    {
        (not null, null) => new[] { model.Name },
        (null, not null) => model.Names,
        (not null, not null) => model.Names.Append(model.Name),
        _ => Enumerable.Empty<string>()
    };

    private static T ReadFromManifestResource<T>(string resourceLocation)
    {
        string resource = ManifestResourceHelper.ReadResource(resourceLocation);
        return deserializer.Deserialize<T>(resource);
    }
}
