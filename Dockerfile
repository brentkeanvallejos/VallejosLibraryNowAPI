FROM	mcr.mir=crosoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

FROM	mcr.microsoft.com.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "VallejosLibraryNowAPI/VallejosLibraryNowAPI.csproj"

RUN dotnet publish "VallejosLibraryNowAPI/VallejosLibraryNowAPI.csproj" -c Release  -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet","VallejosLibraryNowAPI.dll"]



