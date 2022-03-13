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
ENTRYPOINT ["dotnet", "test", "WordTransposerTests.csproj"]