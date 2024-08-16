#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

# 用于支持System.Drawing.Common库，体现为Image.FromFile
# RUN apt-get update && apt-get install -y apt-utils libgdiplus libc6-dev
# RUN ln -s /usr/lib/libgdiplus.so /usr/lib/gdiplus.dll
# RUN ln -s /usr/lib/x86_64-linux-gnu/libdl.so /usr/lib/libdl.dll

EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Gais.Team.Web.Entry/Gais.Team.Web.Entry.csproj", "Gais.Team.Web.Entry/"]
RUN dotnet restore "Gais.Team.Web.Entry/Gais.Team.Web.Entry.csproj"
COPY . .
WORKDIR "/src/Gais.Team.Web.Entry"
RUN dotnet build "Gais.Team.Web.Entry.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gais.Team.Web.Entry.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gais.Team.Web.Entry.dll"]