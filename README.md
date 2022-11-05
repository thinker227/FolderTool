# Folder tool

A command-line tool for displaying a tree graph of the current directory, because `dir` kind of sucks.

> **Warning**
> This tool extensively uses [nerd font](https://www.nerdfonts.com) icons. If you wish to disable icons you can specify the [`--plain`](#plain) option when invoking the tool, or clone the repo and manually edit the [icon resource files](./src/FolderTool/Resources/). The tool is still *usable* without a nerd font, although using one is highly recommended.

## Installation

1. Clone this repo.
2. Run [`install-tool.ps1`](./install-tool.ps1).

```cli
git clone https://github.com/thinker227/FolderTool.git/
cd FolderTool
./install-tool.ps1
```

> **Warning**
> Installation requires the [.NET SDK](https://dotnet.microsoft.com/en-us/) >= 6.0.

## Usage
```cli
folder [<search-pattern>] [options]
```
An example invocation of the tool:
```cli
folder *.cs -r
```

### Arguments
#### `[<search-pattern>]`
The search pattern to display the current directory using, specified as a file search pattern. If [`--regex`](#regex) is specified, the pattern should be a regular expression (.NET flavor).

### Options
#### `-r|--recursive`
Whether to display only the contents of the current directory (default) or of all
sub-directories.

#### `--depth`
The maximum depth a recursive search will enumerate, where 0 will only enumerate the current directory. Only has an effect if [`-r|--recursive`](#r--recursive) is specified.

#### `-m|--path-mode <Full|Name|Relative>`
The mode to display paths using.
- `Full`: Displays the full absolute path to every item.
- `Name`: Display only the name of every item.
- `Relative` (default): Displays the relative path from the current directory to every item.

#### `--plain`
Disables colors and icons when rendering the output. Ideal for terminals which don't support nerd fonts.

#### `--regex`
Whether to treat [`[<search-pattern>]`](#search-pattern) as a Regex (.NET flavor) instead of a file system search pattern. The regex specified will be run on every item and the item will only be displayed if the pattern matches.

#### `--version`
Show version information.

#### `-?|-h|--help`
Show help and usage information.
