FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /App

# Copia os contratos compartilhados
COPY Contracts ./Contracts

# Copia o projeto do operationService
COPY Services/operationService ./Services/operationService

# Define o diretório de trabalho correto antes de restaurar
WORKDIR /App/Services/operationService

# Restaura as dependências
RUN dotnet restore

# Publica a aplicação
RUN dotnet publish -c Release -o /App/out

# Imagem final para runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /App

COPY --from=build /App/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "operationService.dll"]
