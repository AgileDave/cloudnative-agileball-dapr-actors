FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./agileball.runner/agileball.runner.csproj", "./agileball.runner/"]
COPY ["./agileball.interfaces/agileball.interfaces.csproj", "./agileball.interfaces/"]

RUN dotnet restore "./agileball.runner/agileball.runner.csproj"

COPY ./agileball.runner/ ./agileball.runner/
COPY ./agileball.interfaces/ ./agileball.interfaces/

WORKDIR "/src/agileball.runner/."
RUN dotnet build "agileball.runner.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "agileball.runner.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "agileball.runner.dll"]

#to build...
#docker build -t agilewaysreg.azurecr.io/agileball/runner:0.1 --file .\agileball.runner\Dockerfile .