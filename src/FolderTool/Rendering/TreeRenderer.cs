using System.Collections.Generic;
using System.IO;
using FolderTool.Rendering.Icons;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace FolderTool.Rendering;

public sealed class TreeRenderer
{
    private readonly DirectoryInfo rootDirectory;
    private readonly TreeRenderingOptions options;
    private readonly IconRenderer iconRenderer;



    private TreeRenderer(DirectoryInfo rootDirectory, TreeRenderingOptions options)
    {
        this.rootDirectory = rootDirectory;
        this.options = options;
        iconRenderer = new(options.FileIcons, options.DirectoryIcons);
    }



    public static Tree GetTree(DirectoryInfo directory, ISearchPattern searchPattern, TreeRenderingOptions options)
    {
        TreeRenderer renderer = new(directory, options);

        return renderer.GetTree(searchPattern);
    }

    private Tree GetTree(ISearchPattern searchPattern)
    {
        var folder = FolderLocator.GetFolder(rootDirectory, searchPattern);

        var header = GetHeader();
        var tree = new Tree(header)
            .Guide(GetTreeGuide());

        if (folder.Empty)
        {
            tree.AddNode(options.Fancy
                ? "[grey42]<empty>[/]"
                : "<empty>");
        }
        else
        {
            var nodes = GetNodes(folder);
            tree.AddNodes(nodes);
        }

        return tree;
    }

    private IRenderable GetHeader()
    {
        if (options.Fancy)
        {
            return new Inline(
                new Text("\ufb44 "),
                GetTextPath(rootDirectory.FullName));
        }
        else
        {
            return new Text(rootDirectory.FullName);
        }
    }

    private TreeGuide GetTreeGuide() => options.Fancy
        ? RoundedTreeGuide.Custom
        : TreeGuide.Line;

    private IEnumerable<TreeNode> GetNodes(DisplayFolder folder)
    {
        foreach (var subFolder in folder.SubFolders)
        {
            var subNodes = GetNodes(subFolder);

            var display = GetDisplay(subFolder.Directory);
            TreeNode node = new(display);
            node.AddNodes(subNodes);

            yield return node;
        }

        foreach (var file in folder.Files)
        {
            var display = GetDisplay(file);
            yield return new(display);
        }
    }

    private IRenderable GetDisplay(FileSystemInfo entry) => options.Fancy
            ? GetFancyDisplay(entry)
            : GetPlainDisplay(entry);

    private IRenderable GetPlainDisplay(FileSystemInfo entry)
    {
        string name = options.PathDisplayMode switch
        {
            PathDisplayMode.Full => entry.FullName,
            PathDisplayMode.Name => entry.Name,
            PathDisplayMode.Relative or _ =>
                Path.GetRelativePath(rootDirectory.FullName, entry.FullName),
        };

        return new Text(name);
    }

    private IRenderable GetFancyDisplay(FileSystemInfo entry)
    {
        var path = GetFancyTextPath(entry);

        string icon = entry switch
        {
            FileInfo file => iconRenderer.GetFileIcon(file),
            DirectoryInfo directory => iconRenderer.GetDirectoryIcon(directory),
            _ => "?"
        };

        Style iconStyle = entry.Attributes.HasFlag(FileAttributes.Hidden)
            ? new(Color.Grey42)
            : new(Color.White);

        return new Inline(
            new Text($"{icon} ", iconStyle),
            path);
    }

    private IRenderable GetFancyTextPath(FileSystemInfo entry)
    {
        var decoration = entry.Attributes.HasFlag(FileAttributes.Hidden)
            ? Decoration.Italic
            : Decoration.None;

        return options.PathDisplayMode switch
        {
            PathDisplayMode.Full => GetTextPath(entry.FullName, decoration),
            PathDisplayMode.Name => new Text(entry.Name, new Style(decoration: decoration)),
            PathDisplayMode.Relative or _ =>
                GetTextPath(Path.GetRelativePath(rootDirectory.FullName, entry.FullName), decoration),
        };
    }

    private static TextPath GetTextPath(string path, Decoration decoration = Decoration.None) =>
    new(path)
    {
        RootStyle = new(
            foreground: Color.IndianRed,
            decoration: decoration),
        StemStyle = new(
            foreground: Color.Grey42,
            decoration: decoration),
        LeafStyle = new(
            foreground: Color.White,
            decoration: decoration),
        SeparatorStyle = new(
            foreground: Color.Grey42,
            decoration: decoration),
    };
}
