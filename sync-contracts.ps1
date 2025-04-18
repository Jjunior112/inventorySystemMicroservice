# Caminho da pasta Contracts
$contractsDir = ".\Contracts"

# Lista de serviços
$services = @(
  "userService",
  "productService",
  "operationService",
  "stockService"
)

Write-Host "Sincronizando Contracts para os servicos..."

foreach ($service in $services) {
    $dest = ".\$service\Contracts"

    # Remove a pasta antiga se existir
    if (Test-Path $dest) {
        Remove-Item $dest -Recurse -Force
    }

    # Copia a pasta Contracts
    Copy-Item -Path $contractsDir -Destination $dest -Recurse
    Write-Host "✅ Contracts copiados para $service"
}

Write-Host "Sincronizacao concluida!"