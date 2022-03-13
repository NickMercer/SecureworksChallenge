FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.15 AS build
WORKDIR /app

# Copy and restore solution/project files
COPY *.sln .
COPY WordTransposer/*.csproj ./WordTransposer/
COPY WordTransposerTests/*.csproj ./WordTransposerTests/

RUN dotnet restore

#Copy full solution
COPY . .

#build
RUN dotnet build

ENTRYPOINT [ "dotnet", "test" ]