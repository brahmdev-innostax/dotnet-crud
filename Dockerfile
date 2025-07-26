# ---------------- Base Runtime Image ----------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# ---------------- Build Stage ----------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["DotNetCRUD_8.csproj", "."]
RUN dotnet restore "./DotNetCRUD_8.csproj"

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

COPY . .
RUN dotnet build "./DotNetCRUD_8.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Run migrations here (before publishing)
RUN dotnet ef database update

# ---------------- Publish Stage ----------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DotNetCRUD_8.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ---------------- Final Runtime Stage ----------------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "DotNetCRUD_8.dll"]
