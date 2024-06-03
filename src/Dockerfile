FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
#EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/CadastroCliente.Application/CadastroCliente.Application.csproj", "CadastroCliente.Application/"]
COPY ["src/CadastroCliente.Domain/CadastroCliente.Domain.csproj", "CadastroCliente.Domain/"]
COPY ["src/CadastroCliente.Infrastructure.Data/CadastroCliente.Infrastructure.Data.csproj", "CadastroCliente.Infrastructure.Data/"]
RUN dotnet restore "CadastroCliente.Application/CadastroCliente.Application.csproj"
WORKDIR "/src/CadastroCliente.Application"
COPY . .

RUN dotnet build "CadastroCliente.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CadastroCliente.Application.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5000
ENTRYPOINT ["dotnet", "CadastroCliente.Application.dll"]
