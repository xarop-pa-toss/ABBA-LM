# Check .NET SDK version
$requiredVersion = "9.0.0"
$dotnetVersion = dotnet --version

if ($dotnetVersion -lt $requiredVersion) {
    Write-Error ".NET SDK $requiredVersion or higher is required. Please install from https://dotnet.microsoft.com/download"
    exit 1
}

# Install required packages
dotnet add package MongoDB.Driver --version 3.1.0
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Scalar.AspNetCore --version 2.0.1
dotnet add package Swashbuckle.AspNetCore --version 7.0.0

# Restore and build
dotnet restore
dotnet build

Write-Host "API dependencies installed successfully!" -ForegroundColor Green
