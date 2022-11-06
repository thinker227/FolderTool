function Invoke-With-Exit {
	param (
		[scriptblock] $Action
	)
	
	Write-Output "Running '$Action'..."

	$Action.Invoke()

	if ($LASTEXITCODE -ne 0) {
		Exit 1
	}
}

Write-Output "Building and updating tool..."

Invoke-With-Exit {dotnet restore}
Invoke-With-Exit {dotnet build --no-restore --configuration Tool}
Invoke-With-Exit {dotnet pack --no-build}
Invoke-With-Exit {dotnet tool update --global --add-source ./src/FolderTool/bin/package FolderTool --prerelease}
