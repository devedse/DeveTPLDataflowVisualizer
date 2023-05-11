$ErrorActionPreference = "Stop"

$invocation = (Get-Variable MyInvocation).Value
$directorypath = Split-Path $invocation.MyCommand.Path
$solutionRoot = Split-Path -Path $directorypath -Parent

$powerShellHelpersModule = Join-Path $directorypath "PowerShellHelpers"
Import-Module -Name $powerShellHelpersModule

$outputDir = Join-Path $directorypath "Output"
$7zFilePath = Join-Path $outputDir "DeveMazeGeneratorCore.7z"
$zipFilePath = Join-Path $outputDir "DeveMazeGeneratorCore.zip"

DeleteFileIfExists $7zFilePath
DeleteFileIfExists $zipFilePath
DeleteFolderIfExists $outputDir

$buildPath = Join-Path $solutionRoot "DeveMazeGeneratorCore\bin\Release"

# Exclude *.pdb files
7z a -mm=Deflate -mfb=258 -mpass=15 "$zipFilePath" "$buildPath\*" '-x!*.pdb'
7z a -t7z -m0=LZMA2 -mmt=on -mx9 -md=1536m -mfb=273 -ms=on -mqs=on -sccUTF-8 "$7zFilePath" "$buildPath\*" '-x!*.pdb'