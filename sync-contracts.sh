#!/bin/bash

# Caminho da pasta Contracts
CONTRACTS_DIR="./Contracts"

# Lista de serviços que precisam dos contratos
SERVICES=(
  "userService"
  "productService"
  "operationService"
  "stockService"
)

echo "📦 Sincronizando Contracts para os serviços..."

for SERVICE in "${SERVICES[@]}"
do
  DEST="./$SERVICE/Contracts"

  # Remove a pasta antiga se existir
  if [ -d "$DEST" ]; then
    rm -rf "$DEST"
  fi

  # Copia a pasta Contracts para dentro do serviço
  cp -r "$CONTRACTS_DIR" "$DEST"
  echo "✅ Contracts copiados para $SERVICE"
done

echo "🚀 Sincronização concluída!"