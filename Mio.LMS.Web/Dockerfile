﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Mio.LMS.Web/Mio.LMS.Web.csproj", "Mio.LMS.Web/"]
RUN dotnet restore "Mio.LMS.Web/Mio.LMS.Web.csproj"
COPY . .
WORKDIR "/src/Mio.LMS.Web"
RUN dotnet build "Mio.LMS.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Mio.LMS.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mio.LMS.Web.dll"]
