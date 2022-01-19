#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MicroMachinesUserService/MicroMachinesUserService.csproj", "MicroMachinesUserService/"]
RUN dotnet restore "MicroMachinesUserService/MicroMachinesUserService.csproj"
COPY . .
WORKDIR "/src/MicroMachinesUserService"
RUN dotnet build "MicroMachinesUserService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MicroMachinesUserService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroMachinesUserService.dll"]