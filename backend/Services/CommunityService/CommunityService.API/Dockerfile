FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CommunityService.API/CommunityService.API.csproj", "CommunityService.API/"]
COPY ["CommunityService.BLL/CommunityService.BLL.csproj", "CommunityService.BLL/"]
COPY ["CommunityService.CORE/CommunityService.CORE.csproj", "CommunityService.CORE/"]
COPY ["CommunityService.DAL/CommunityService.DAL.csproj", "CommunityService.DAL/"]
RUN dotnet restore "CommunityService.API/CommunityService.API.csproj"
COPY . .
WORKDIR "/src/CommunityService.API"
RUN dotnet build "./CommunityService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CommunityService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommunityService.API.dll"]
