using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.IO;
using System.Text;
using FolderTool;
using FolderTool.Installation;
using FolderTool.Rendering;
using Spectre.Console;

RootCommand rootCommand = new()
{
	Name = "folder",
	Description = "Displays a tree of files and directories in the current directory"
};

Argument<string> searchPatternArgument = new()
{
	Name = "search-pattern",
	Description = "The search pattern to display the current directory using",
};
searchPatternArgument.SetDefaultValue("");
rootCommand.AddArgument(searchPatternArgument);

Option<bool> recursiveOption = new("--recursive")
{
    Description = "Whether to display only the contents of the current directory (default) or of all sub-directories"
};
recursiveOption.SetDefaultValue(false);
recursiveOption.AddAlias("-r");
rootCommand.AddOption(recursiveOption);

Option<int> maxDepthOption = new("--depth")
{
    Description = "The maximum depth a recursive search will enumerate, where 0 will only enumerate the current directory. Only has an effect if -r|--recursive is specified"
};
maxDepthOption.SetDefaultValue(int.MaxValue);
rootCommand.AddOption(maxDepthOption);

Option<PathDisplayMode> pathDisplayModeOption = new("--path-mode")
{
    Description = "The mode to display paths using"
};
pathDisplayModeOption.SetDefaultValue(PathDisplayMode.Relative);
pathDisplayModeOption.AddAlias("-m");
rootCommand.AddOption(pathDisplayModeOption);

Option<bool> noFancyOption = new("--plain")
{
    Description = "Disables colors and icons"
};
noFancyOption.SetDefaultValue(false);
rootCommand.AddOption(noFancyOption);

Option<bool> regexOption = new("--regex")
{
    Description = "Whether to treat the search pattern as a Regex instead of a file system search pattern"
};
rootCommand.AddOption(regexOption);

rootCommand.SetHandler((searchPattern, recursive, maxDepth, pathDisplayMode, noFancy, regex) =>
{
    DirectoryInfo currentDirectory = new(Directory.GetCurrentDirectory());

    ISearchPattern pattern = regex
        ? RegexSearchPattern.Create(
            searchPattern,
            recursive,
            maxDepth)
        : new FileSystemSearchPattern(
            string.IsNullOrWhiteSpace(searchPattern) ? "*" : searchPattern,
            recursive,
            maxDepth);

    TreeRenderingOptions options = new()
    {
        Fancy = !noFancy,
        PathDisplayMode = pathDisplayMode
    };

    var tree = TreeRenderer.GetTree(currentDirectory, pattern, options);

    if (options.Fancy)
    {
        Console.OutputEncoding = Encoding.UTF8;
    }

    AnsiConsole.Write(tree);
},
	searchPatternArgument,
    recursiveOption,
    maxDepthOption,
    pathDisplayModeOption,
    noFancyOption,
    regexOption);

Command installCommand = new("install")
{
    Description = "Installs the tool in the current directory"
};
installCommand.SetHandler(() =>
{
    DirectoryInfo currentDirectory = new(Directory.GetCurrentDirectory());

    Installer.Install(currentDirectory);
});
rootCommand.AddCommand(installCommand);

CommandLineBuilder builder = new(rootCommand);

builder.UseDefaults();

var parser = builder.Build();

return parser.Invoke(args);
