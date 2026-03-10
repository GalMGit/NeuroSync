FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CommentService.API/CommentService.API.csproj", "CommentService.API/"]
COPY ["CommentService.BLL/CommentService.BLL.csproj", "CommentService.BLL/"]
COPY ["CommentService.CORE/CommentService.CORE.csproj", "CommentService.CORE/"]
COPY ["CommentService.DAL/CommentService.DAL.csproj", "CommentService.DAL/"]
RUN dotnet restore "CommentService.API/CommentService.API.csproj"
COPY . .
WORKDIR "/src/CommentService.API"
RUN dotnet build "./CommentService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CommentService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CommentService.API.dll"]
