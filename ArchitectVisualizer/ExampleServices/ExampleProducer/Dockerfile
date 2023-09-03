FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ExampleServices/ExampleProducer/ExampleProducer.csproj", "ExampleServices/ExampleProducer/"]
RUN dotnet restore "ExampleServices/ExampleProducer/ExampleProducer.csproj"
COPY . .
WORKDIR "/src/ExampleServices/ExampleProducer"
RUN dotnet build "ExampleProducer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExampleProducer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExampleProducer.dll"]
