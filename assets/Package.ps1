<#
.SYNOPSIS
    Packages the Stacks Azure Event Hub NuGet packages.
#>
param (
    $Configuration = "DEBUG",
    $Packages = @("Slalom.Stacks.EventHub")
)

function Clear-LocalCache() {
    $paths = nuget locals all -list
    foreach($path in $paths) {
        $path = $path.Substring($path.IndexOf(' ')).Trim()

        if (Test-Path $path) {

            Push-Location $path

            foreach($package in $Packages) {

                foreach($item in Get-ChildItem -Filter "$package" -Recurse) {
                    if (Test-Path $item) {
                        Remove-Item $item.FullName -Recurse -Force
                        Write-Host "Removing $item"
                    }
                }
            }

            Pop-Location
        }
    }
}

function Go ($Path) {
    Push-Location $Path

    Remove-Item .\Bin -Force -Recurse
    Clear-LocalCache
    dotnet build
    dotnet pack --no-build --configuration $Configuration
    copy .\bin\$Configuration\*.nupkg c:\nuget\

    Pop-Location
}

Push-Location $PSScriptRoot

foreach($package in $Packages) {
    Go "..\src\$package"
}

Pop-Location



