#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DeveTPLDataflowVisualizer.ConsoleApp/DeveTPLDataflowVisualizer.ConsoleApp.csproj", "DeveTPLDataflowVisualizer.ConsoleApp/"]
COPY ["DeveTPLDataflowVisualizer/DeveTPLDataflowVisualizer.csproj", "DeveTPLDataflowVisualizer/"]
RUN dotnet restore "DeveTPLDataflowVisualizer.ConsoleApp/DeveTPLDataflowVisualizer.ConsoleApp.csproj"
COPY . .
WORKDIR "/src/DeveTPLDataflowVisualizer.ConsoleApp"
RUN dotnet build "DeveTPLDataflowVisualizer.ConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_VERSION
ARG VER=${BUILD_VERSION:-1.0.0}
RUN dotnet publish "DeveTPLDataflowVisualizer.ConsoleApp.csproj" -c Release -o /app/publish /p:Version=$VER

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DeveTPLDataflowVisualizer.ConsoleApp.dll"]
