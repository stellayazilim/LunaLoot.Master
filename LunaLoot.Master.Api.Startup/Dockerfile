FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LunaLoot.Api.Startup/LunaLoot.Api.Startup.csproj", "LunaLoot.Api.Startup/"]
RUN dotnet restore "LunaLoot.Api.Startup/LunaLoot.Api.Startup.csproj"
COPY . .
WORKDIR "/src/LunaLoot.Api.Startup"
RUN dotnet build "LunaLoot.Api.Startup.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LunaLoot.Api.Startup.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LunaLoot.Api.Startup.dll"]
