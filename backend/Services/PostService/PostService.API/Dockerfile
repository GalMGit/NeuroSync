FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PostService.API/PostService.API.csproj", "PostService.API/"]
COPY ["PostService.BLL/PostService.BLL.csproj", "PostService.BLL/"]
COPY ["PostService.CORE/PostService.CORE.csproj", "PostService.CORE/"]
COPY ["PostService.DAL/PostService.DAL.csproj", "PostService.DAL/"]
RUN dotnet restore "PostService.API/PostService.API.csproj"
COPY . .
WORKDIR "/src/PostService.API"
RUN dotnet build "./PostService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PostService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostService.API.dll"]
