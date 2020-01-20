FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /usr
COPY cert.pem /usr/local/share/ca-certificates/cert.crt
RUN update-ca-certificates

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY vgs-card-example-asp-net/vgs-card-example-asp-net.csproj vgs-card-example-asp-net/
RUN dotnet restore "vgs-card-example-asp-net/vgs-card-example-asp-net.csproj"
COPY . .
WORKDIR "/src/vgs-card-example-asp-net"
RUN dotnet build "vgs-card-example-asp-net.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "vgs-card-example-asp-net.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vgs-card-example-asp-net.dll"]
