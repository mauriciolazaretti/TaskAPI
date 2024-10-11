# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
WORKDIR /
COPY ./aspnetapp.pfx /usr/local/share/ca-certificates/aspnetapp.pfx
RUN apk update && apk add ca-certificates
RUN update-ca-certificates

# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TaskAPI/TaskAPI.csproj", "TaskAPI/"]
COPY ["TaskAPI.DataAccess/TaskAPI.DataAccess.csproj", "TaskAPI.DataAccess/"]
COPY ["TaskAPI.Services/TaskAPI.Services.csproj", "TaskAPI.Services/"]
RUN dotnet restore "./TaskAPI/TaskAPI.csproj"
COPY . .
WORKDIR "/src/TaskAPI"
RUN dotnet build "./TaskAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./TaskAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskAPI.dll"]