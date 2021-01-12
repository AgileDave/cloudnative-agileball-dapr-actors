FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./agileball.service/agileball.service.csproj", "./agileball.service/"]
COPY ["./agileball.interfaces/agileball.interfaces.csproj", "./agileball.interfaces/"]

RUN dotnet restore "./agileball.service/agileball.service.csproj"

COPY ./agileball.service/ ./agileball.service/
COPY ./agileball.interfaces/ ./agileball.interfaces/

WORKDIR "/src/agileball.service/."
RUN dotnet build "agileball.service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "agileball.service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "agileball.service.dll"]

#to build...
#docker build -t agilewaysreg.azurecr.io/agileball/actors:0.1 --file .\agileball.service\Dockerfile .