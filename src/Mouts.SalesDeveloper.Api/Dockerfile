# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

# Build image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files first
COPY Mouts.SalesDeveloper.Api/Mouts.SalesDeveloper.Api.csproj Mouts.SalesDeveloper.Api/
COPY Mouts.SalesDeveloper.Application/Mouts.SalesDeveloper.Application.csproj Mouts.SalesDeveloper.Application/
COPY Mouts.SalesDeveloper.Domain/Mouts.SalesDeveloper.Domain.csproj Mouts.SalesDeveloper.Domain/
COPY Mouts.SalesDeveloper.Infrastructure/Mouts.SalesDeveloper.Infrastructure.csproj Mouts.SalesDeveloper.Infrastructure/

RUN dotnet restore Mouts.SalesDeveloper.Api/Mouts.SalesDeveloper.Api.csproj

# Copy everything else
COPY . .

WORKDIR /src/Mouts.SalesDeveloper.Api
RUN dotnet build "Mouts.SalesDeveloper.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish image
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Mouts.SalesDeveloper.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Mouts.SalesDeveloper.Api.dll"]