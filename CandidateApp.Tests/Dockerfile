# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy only the necessary project files to leverage Docker cache
COPY ["CandidateApp.Tests/CandidateApp.Tests.csproj", "CandidateApp.Tests/"]
COPY ["CandidateApp.Api/CandidateApp.Api.csproj", "CandidateApp.Api/"]
COPY ["CandidateApp.Application/CandidateApp.Application.csproj", "CandidateApp.Application/"]
COPY ["CandidateApp.Domain/CandidateApp.Domain.csproj", "CandidateApp.Domain/"]
COPY ["CandidateApp.Infrastructure/CandidateApp.Infrastructure.csproj", "CandidateApp.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "CandidateApp.Tests/CandidateApp.Tests.csproj" --no-cache

# Copy the remaining source code and build
COPY . . 
WORKDIR "/src/CandidateApp.Tests"
RUN dotnet build "./CandidateApp.Tests.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Final stage: Use dotnet test
FROM build AS final
WORKDIR /src/CandidateApp.Tests
CMD ["dotnet", "test", "--verbosity", "detailed"]
