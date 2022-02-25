FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /src
COPY ["EtteplanMORE.ServiceManual.Web/EtteplanMORE.ServiceManual.Web.csproj", "EtteplanMORE.ServiceManual.Web/"]
COPY ["EtteplanMORE.ServiceManual.ApplicationCore/EtteplanMORE.ServiceManual.ApplicationCore.csproj", "EtteplanMORE.ServiceManual.ApplicationCore/"]
RUN dotnet restore "EtteplanMORE.ServiceManual.Web/EtteplanMORE.ServiceManual.Web.csproj"

COPY . .
WORKDIR "/src/EtteplanMORE.ServiceManual.Web"
RUN dotnet build "EtteplanMORE.ServiceManual.Web.csproj" -c Release -o /app

FROM build-env AS publish
RUN dotnet publish "EtteplanMORE.ServiceManual.Web.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EtteplanMORE.ServiceManual.Web.dll"]