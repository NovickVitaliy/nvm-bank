FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Users/Users.API/Users.API.csproj", "Services/Users/Users.API/"]
COPY ["Common/Common.CQRS/Common.CQRS.csproj", "Common/Common.CQRS/"]
COPY ["Common/Common/Common.csproj", "Common/Common/"]
COPY ["Common/Common.Auth/Common.Auth.csproj", "Common/Common.Auth/"]
COPY ["Common/Common.Messaging/Common.Messaging.csproj", "Common/Common.Messaging/"]
RUN dotnet restore "Services/Users/Users.API/Users.API.csproj"
COPY . .
WORKDIR "/src/Services/Users/Users.API"
RUN dotnet build "Users.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Users.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.API.dll"]
