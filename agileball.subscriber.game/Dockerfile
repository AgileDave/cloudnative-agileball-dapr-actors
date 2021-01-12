FROM mcr.microsoft.com/dotnet/aspnet:3.1-alpine AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./agileball.subscriber.game/agileball.subscriber.game.csproj", "./agileball.subscriber.game/"]
COPY ["./agileball.interfaces/agileball.interfaces.csproj", "./agileball.interfaces/"]

RUN dotnet restore "./agileball.subscriber.game/agileball.subscriber.game.csproj"

COPY ./agileball.subscriber.game/ ./agileball.subscriber.game/
COPY ./agileball.interfaces/ ./agileball.interfaces/

WORKDIR "/src/agileball.subscriber.game/."
RUN dotnet build "agileball.subscriber.game.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "agileball.subscriber.game.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "agileball.subscriber.game.dll"]

#to build...
#docker build -t agilewaysreg.azurecr.io/agileball/subscriber/game:0.1 --file .\agileball.subscriber.game\Dockerfile .