FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /App

# Copia apenas os contratos compartilhados
COPY Contracts ./Contracts

# Copia o projeto específico do userService
COPY Services/userService ./Services/userService

# Define o diretório de trabalho no projeto copiado
WORKDIR /App/Services/userService

# Restaura os pacotes
RUN dotnet restore

# Publica a aplicação em modo Release
RUN dotnet publish -c Release -o /App/out

# Imagem final para runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /App

COPY --from=build /App/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "userService.dll"]
