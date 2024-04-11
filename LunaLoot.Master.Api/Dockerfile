FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LunaLoot.Master.Api/LunaLoot.Master.Api.csproj", "LunaLoot.Master.Api/"]
RUN dotnet restore "LunaLoot.Master.Api/LunaLoot.Master.Api.csproj"
COPY . .
WORKDIR "/src/LunaLoot.Master.Api"
RUN dotnet build "LunaLoot.Master.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LunaLoot.Master.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LunaLoot.Master.Api.dll"]
