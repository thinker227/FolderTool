$sourceDir = "./src/FolderTool"
$publishDir = "$sourceDir/bin/publish"
$archs = "win-x64","win-x86","linux-x64","linux-arm","linux-arm64","osx-x64","osx.12-arm64"

foreach ($arch in $archs) {
    $archDir = "$publishDir/$arch"
    Write-Output "Publishing for $arch"

    dotnet publish ./src/FolderTool -o $archDir -r $arch -c Release -p:DebugType=None -p:PublishSingleFile=true --self-contained true --nologo
}
