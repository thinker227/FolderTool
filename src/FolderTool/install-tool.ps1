Write-Output "Building and installing tool..."

dotnet restore
dotnet build --no-restore
dotnet pack --no-build
dotnet tool install --global --add-source ./bin/package FolderTool --prerelease
