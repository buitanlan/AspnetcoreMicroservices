FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Services/Inventory.Product.API/Inventory.Product.API.csproj", "src/Services/Inventory.Product.API/"]
RUN dotnet restore "src/Services/Inventory.Product.API/Inventory.Product.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Inventory.Product.API"
RUN dotnet build "Inventory.Product.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Inventory.Product.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Inventory.Product.API.dll"]
