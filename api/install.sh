#!/bin/bash

# Function to detect OS
detect_os() {
    case "$(uname -s)" in
        Linux*)     OS='Linux';;
        Darwin*)    OS='Mac';;
        *)         OS='Unknown';;
    esac
    echo $OS
}

# Install .NET SDK based on OS
install_dotnet() {
    OS=$(detect_os)
    case $OS in
        'Linux')
            # Install .NET SDK on Linux
            wget https://dot.net/v1/dotnet-install.sh
            chmod +x dotnet-install.sh
            ./dotnet-install.sh --channel 9.0
            rm dotnet-install.sh
            ;;
        'Mac')
            # Install .NET SDK on macOS
            if command -v brew &> /dev/null; then
                brew install dotnet@9.0
            else
                echo "Please install Homebrew first: https://brew.sh/"
                exit 1
            fi
            ;;
        *)
            echo "Unsupported operating system"
            exit 1
            ;;
    esac
}

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo ".NET SDK not found. Installing..."
    install_dotnet
fi

# Check .NET version
DOTNET_VERSION=$(dotnet --version)
if [ "${DOTNET_VERSION%.*}" -lt "9" ]; then
    echo ".NET 9.0+ is required. Current version: $DOTNET_VERSION"
    install_dotnet
fi

# Install NuGet packages
dotnet add package MongoDB.Driver --version 3.1.0
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Scalar.AspNetCore --version 2.0.1
dotnet add package Swashbuckle.AspNetCore --version 7.0.0

# Restore and build
dotnet restore
dotnet build

echo "API dependencies installed successfully!"
