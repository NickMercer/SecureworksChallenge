FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

COPY WordTransposer/*.csproj WordTransposer/
RUN dotnet restore WordTransposer/WordTransposer.csproj


COPY WordTransposer/ WordTransposer/
WORKDIR /source/WordTransposer
RUN dotnet build -c release --no-restore

FROM build AS test
WORKDIR /source/WordTransposerTests
COPY WordTransposerTests/ .
ENTRYPOINT ["dotnet", "test", "--logger:trx"]

FROM build AS publish
RUN dotnet publish -c release --no-build -o /app

FROM mcr.microsoft.com/dotnet/runtime:6.0
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WordTransposer.dll"]