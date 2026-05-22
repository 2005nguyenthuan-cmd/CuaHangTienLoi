param(
    [string]$Server = "localhost",
    [string]$Database = "CUA_HANG_TIEN_LOI"
)

$ErrorActionPreference = "Stop"

$scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$schemaFile = Join-Path $scriptRoot "CUA_HANG_TIEN_LOI.sql"
$inventoryFile = Join-Path $scriptRoot "THEM_KIEM_KE.sql"

Write-Host "Resetting database $Database on $Server..."

$dropQuery = @"
IF DB_ID(N'$Database') IS NOT NULL
BEGIN
    ALTER DATABASE [$Database] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$Database];
END
"@

sqlcmd -S $Server -d master -Q $dropQuery -b
if ($LASTEXITCODE -ne 0) { throw "Failed to drop existing database." }

sqlcmd -S $Server -d master -i $schemaFile -f 65001 -b
if ($LASTEXITCODE -ne 0) { throw "Failed to import schema file." }

sqlcmd -S $Server -d $Database -i $inventoryFile -f 65001 -b
if ($LASTEXITCODE -ne 0) { throw "Failed to import inventory file." }

Write-Host "Database reset completed."
