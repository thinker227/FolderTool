using System.IO;

namespace FolderTool;

public static class Icons
{
    public static string GetFileIcon(FileInfo file) => file.Extension switch
    {
        ".cs" => "\uf81a",
        ".csproj" => "\ue70c",
        ".ps1" => "\uebc7",
        ".json" => "\ue60b",
        ".md" => "\ue73e",
        ".txt" => "\uf0f6",
        ".mp3" or ".wav" or ".flac" => "\uf722",
        ".mp4" => "\uf1c8",
        ".png" or ".jpeg" or ".jpg" or ".ico" or ".kra" => "\uf1c5",
        _ => "\uea7b"
    };

    public static string GetDirectoryIcon(DirectoryInfo directory) => directory.Name switch
    {
        "obj" or "bin" => "\ue5fc",
        "Properties" => "\ufab6",
        _ => "\uf74a"
    };
}
