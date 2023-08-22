FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
# # insrakk CA
# RUN apt-get update && \
#     apt-get install -y ca-certificates gnupg curl wget --no-install-recommends && \
#     rm -rf /var/lib/apt/lists/*

# Install curl
RUN apt-get update && apt-get install -y curl

# Install Node.js
RUN curl -fsSL --insecure https://deb.nodesource.com/setup_18.x | bash - \
    && apt-get install -y \
        nodejs \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /src
COPY ["ArchitectVisualizer/ArchitectVisualizer.csproj", "ArchitectVisualizer/"]
COPY . .

RUN dotnet restore --configfile "./NuGet.Config" --verbosity diagnostic "ArchitectVisualizer/ArchitectVisualizer.csproj"

WORKDIR "/src/ArchitectVisualizer"
RUN dotnet build "ArchitectVisualizer.csproj" -c Release -o /app/build

# ENV NODE_TLS_REJECT_UNAUTHORIZED=0

# RUN apt-get update && apt-get install -y ca-certificates
RUN npm config set strict-ssl false

FROM build AS publish
RUN dotnet publish "ArchitectVisualizer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArchitectVisualizer.dll"]
