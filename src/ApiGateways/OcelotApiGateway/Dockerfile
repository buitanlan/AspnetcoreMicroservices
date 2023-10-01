FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/OcelotApiGateway/OcelotApiGateway.csproj", "src/ApiGateways/OcelotApiGateway/"]
RUN dotnet restore "src/ApiGateways/OcelotApiGateway/OcelotApiGateway.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/OcelotApiGateway"
RUN dotnet build "OcelotApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OcelotApiGateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OcelotApiGateway.dll"]
