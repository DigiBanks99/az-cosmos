FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/AzCosmos.WebApp/AzCosmos.WebApp.csproj", "src/AzCosmos.WebApp/"]
RUN dotnet restore "src/AzCosmos.WebApp/AzCosmos.WebApp.csproj"
COPY . .
WORKDIR "/src/src/AzCosmos.WebApp"
RUN dotnet build "AzCosmos.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AzCosmos.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AzCosmos.WebApp.dll"]
