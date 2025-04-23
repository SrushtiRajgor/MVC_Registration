Import-Module WebAdministration

$siteName = "app.srustiReg.com"
$sourcePath = "D:\MVC_Registration-main\MVC_Registration-main\Registration1_Completed\Registration1\bin\app.publish"
$destinationPath = "C:\inetpub\wwwroot\HUBDash"
$timestamp = Get-Date -Format "yyyyMMddHHmmss"
$backupPath = "C:\inetpub\wwwroot\HUBDash-backup-$timestamp"

Write-Output "Stopping IIS site: $siteName"
Stop-Website -Name $siteName


if (!(Test-Path $destinationPath)) {
    Write-Output "Creating destination folder: $destinationPath"
    New-Item -Path $destinationPath -ItemType Directory -Force | Out-Null
}

if (Test-Path $destinationPath) {
    Write-Output "Backing up existing deployment to: $backupPath"
    Copy-Item -Path $destinationPath -Destination $backupPath -Recurse -Force
}


Write-Output "Removing old files from deployment folder..."
Remove-Item "$destinationPath\*" -Recurse -Force


Write-Output "Copying new files from: $sourcePath"
Copy-Item -Path "$sourcePath\*" -Destination $destinationPath -Recurse -Force


Write-Output "Starting IIS site: $siteName"
Start-Website -Name $siteName


Write-Output "✅ Deployment completed successfully."
