FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY "./OrdersService.API/*.csproj" "./OrdersService.API/"
COPY "./OrdersService.Application/*.csproj" "./OrdersService.Application/"
COPY "./OrdersService.Infrastructure/*.csproj" "./OrdersService.Infrastructure/"
COPY "./OrdersService.Domain/*.csproj" "./OrdersService.Domain/"

RUN dotnet restore "./OrdersService.API/OrdersService.API.csproj"

COPY . .

RUN dotnet publish "OrdersService.API/OrdersService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "OrdersService.API.dll"]
