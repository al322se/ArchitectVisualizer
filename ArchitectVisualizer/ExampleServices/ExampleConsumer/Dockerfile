FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ExampleServices/ExampleConsumer/ExampleConsumer.csproj", "ExampleServices/ExampleConsumer/"]
RUN dotnet restore "ExampleServices/ExampleConsumer/ExampleConsumer.csproj"
COPY . .
WORKDIR "/src/ExampleServices/ExampleConsumer"
RUN dotnet build "ExampleConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExampleConsumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExampleConsumer.dll"]
