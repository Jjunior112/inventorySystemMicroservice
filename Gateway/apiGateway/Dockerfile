FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /App

# Copia os contratos compartilhados (se o gateway os usa)
COPY Contracts ./Contracts

# Copia o projeto do API Gateway
COPY Gateway/apiGateway ./Gateway/apiGateway

# Define o diretório de trabalho do projeto
WORKDIR /App/Gateway/apiGateway

# Restaura as dependências
RUN dotnet restore

# Publica a aplicação
RUN dotnet publish -c Release -o /App/out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /App

COPY --from=build /App/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "apiGateway.dll"]
