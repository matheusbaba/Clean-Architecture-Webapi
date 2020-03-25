FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY CarRentalDDD.API/CarRentalDDD.API.csproj ./CarRentalDDD.API/
COPY CarRentalDDD.Domain/CarRentalDDD.Domain.csproj ./CarRentalDDD.Domain/
COPY CarRentalDDD.Infra/CarRentalDDD.Infra.csproj ./CarRentalDDD.Infra/

RUN dotnet restore CarRentalDDD.Domain/CarRentalDDD.Domain.csproj
RUN dotnet restore CarRentalDDD.Infra/CarRentalDDD.Infra.csproj
RUN dotnet restore CarRentalDDD.API/CarRentalDDD.API.csproj

COPY . .
WORKDIR /src/CarRentalDDD.API
RUN dotnet build CarRentalDDD.API.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarRentalDDD.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarRentalDDD.API.dll"]
