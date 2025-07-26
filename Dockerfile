# ---------------- Base Runtime Image ----------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# ---------------- Build Stage ----------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["DotNetCRUD_8.csproj", "."]
RUN dotnet restore "./DotNetCRUD_8.csproj"

#  Install EF Tools (for migrations)
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Copy remaining source and build
COPY . .
RUN dotnet build "./DotNetCRUD_8.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ---------------- Publish Stage ----------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DotNetCRUD_8.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ---------------- Final Runtime Stage ----------------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Run migrations first, then start API
ENTRYPOINT ["sh", "-c", "dotnet ef database update && dotnet DotNetCRUD_8.dll"]
