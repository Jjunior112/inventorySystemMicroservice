FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /App

# Copia somente os arquivos da authService para dentro de /App (diretório atual)
COPY Services/authService . 

# Aqui o WORKDIR já está em /App, então rodamos os comandos ali
RUN dotnet restore

RUN dotnet publish -c Release -o /App/out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /App

COPY --from=build /App/out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "authService.dll"]
