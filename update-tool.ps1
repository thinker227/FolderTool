Write-Output "Building and updating tool..."

dotnet restore
dotnet build --no-restore
dotnet pack --no-build
dotnet tool update --global --add-source ./src/FolderTool/bin/package FolderTool --prerelease
