#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MicroMachinesStockService/MicroMachinesStockService.csproj", "MicroMachinesStockService/"]
RUN dotnet restore "MicroMachinesStockService/MicroMachinesStockService.csproj"
COPY . .
WORKDIR "/src/MicroMachinesStockService"
RUN dotnet build "MicroMachinesStockService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroMachinesStockService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroMachinesStockService.dll"]