# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file
COPY LMWebAPI.sln ./

# Copy project files for proper restore
COPY LMWebAPI/LMWebAPI.csproj LMWebAPI/
COPY BloodTourney/BloodTourney.csproj BloodTourney/
COPY BloodTourney.Tests/BloodTourney.Tests.csproj BloodTourney.Tests/
# If you have other class libraries, add them here
# COPY OtherProject/OtherProject.csproj OtherProject/

# Restore dependencies
RUN dotnet restore LMWebAPI.sln

# Copy the rest of the source code
COPY . .

# Publish the API project
WORKDIR /src/LMWebAPI
RUN dotnet publish LMWebAPI.csproj -c Release -o /app

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy compiled output from build stage
COPY --from=build /app .

# Expose port 80 for HTTP
EXPOSE 80

# Set entry point
ENTRYPOINT ["dotnet", "LMWebAPI.dll"]
