FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY "./PaymentsService.API/*.csproj" "./PaymentsService.API/"
COPY "./PaymentsService.Application/*.csproj" "./PaymentsService.Application/"
COPY "./PaymentsService.Infrastructure/*.csproj" "./PaymentsService.Infrastructure/"
COPY "./PaymentsService.Domain/*.csproj" "./PaymentsService.Domain/"

RUN dotnet restore "./PaymentsService.API/PaymentsService.API.csproj"

COPY . .

RUN dotnet publish "PaymentsService.API/PaymentsService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PaymentsService.API.dll"]
