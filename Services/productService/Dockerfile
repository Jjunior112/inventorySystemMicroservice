FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /App

# Copia os contratos compartilhados
COPY Contracts ./Contracts

# Copia o projeto do microserviço
COPY Services/productService ./Services/productService

# Restaura as dependências
WORKDIR /App/Services/productService
RUN dotnet restore

# Publica a aplicação
RUN dotnet publish -c Release -o /App/out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /App

COPY --from=build /App/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "productService.dll"]