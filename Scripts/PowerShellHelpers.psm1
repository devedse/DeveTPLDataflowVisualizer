Function DeleteFileIfExists($path) {
    if (Test-Path $path) {
        Remove-Item -Path $path -Force
    }
}

Function DeleteFolderIfExists($path) {
    if (Test-Path $path) {
        Remove-Item -LiteralPath $path -Force -Recurse
    }
}