FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Netatmo.Dashboard.Api/Netatmo.Dashboard.Api.csproj", "Netatmo.Dashboard.Api/"]
RUN dotnet restore "Netatmo.Dashboard.Api/Netatmo.Dashboard.Api.csproj"
COPY . .
WORKDIR "/src/Netatmo.Dashboard.Api"
RUN dotnet build "Netatmo.Dashboard.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Netatmo.Dashboard.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Netatmo.Dashboard.Api.dll"]