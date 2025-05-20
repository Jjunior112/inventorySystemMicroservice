FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /App

# Copia apenas os contratos compartilhados
COPY Contracts ./Contracts

# Copia o projeto do stockService
COPY Services/stockService ./Services/stockService

# Define o diretório de trabalho para o projeto
WORKDIR /App/Services/stockService

# Restaura as dependências
RUN dotnet restore

# Publica o projeto
RUN dotnet publish -c Release -o /App/out

# Imagem final de runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /App

COPY --from=build /App/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "stockService.dll"]

