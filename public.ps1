Write-Host "=== Compilando y publicando API para Linux ==="

# Cambiar al directorio del script
Set-Location -Path $PSScriptRoot

# Ejecutar publicación
dotnet publish .\webapi\cndcAPI.csproj -c Release -r linux-x64 --self-contained:$false -o .\publish

Write-Host "=== Publicación finalizada en .\publish ==="