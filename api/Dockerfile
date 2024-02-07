ARG DOTNET_VERSION=8.0

############ Base
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION}-alpine AS base
WORKDIR /app
EXPOSE 3000/tcp
#VOLUME /etc/bdtheque
#VOLUME /etc/bdtheque/secrets

############ Build
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS build
WORKDIR /src
COPY . .
RUN dotnet restore "BDTheque.net.sln"

FROM build AS publish
COPY . .
WORKDIR "/src/BDTheque.Web"
RUN dotnet publish "BDTheque.Web.csproj" -c Release --no-restore -o /app/publish

############ Production
FROM base AS production
RUN apk --no-cache add curl
HEALTHCHECK --interval=1m --timeout=30s --retries=3 --start-period=5s CMD curl --fail http://localhost:3000/health || exit 1
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BDTheque.Web.dll"]


