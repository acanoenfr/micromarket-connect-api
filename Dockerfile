FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /app
COPY . ./

RUN dotnet restore "Com.MicroMarketConnect.API.Web/Com.MicroMarketConnect.API.Web.csproj"
RUN dotnet publish "Com.MicroMarketConnect.API.Web/Com.MicroMarketConnect.API.Web.csproj" --no-restore -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime

WORKDIR /app

COPY --from=build /app .

EXPOSE 80
ENV ASPNETCORE_HTTP_PORTS=80
ENTRYPOINT ["dotnet", "Com.MicroMarketConnect.API.Web.dll"]
