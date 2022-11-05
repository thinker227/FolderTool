using System;
using Spectre.Console;
using Spectre.Console.Rendering;

namespace FolderTool.Rendering;

internal sealed class RoundedTreeGuide : TreeGuide
{
    public static RoundedTreeGuide Custom { get; } = new();

    private RoundedTreeGuide() { }

    public override string GetPart(TreeGuidePart part) => part switch
    {
        TreeGuidePart.Space =>    "    ",
        TreeGuidePart.Continue => "│   ",
        TreeGuidePart.Fork =>     "├──╴",
        TreeGuidePart.End =>      "╰──╴",
        _ => throw new InvalidOperationException()
    };
}
