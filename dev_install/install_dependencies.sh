#!/bin/bash

echo "====================================="
echo "ABBA League Manager Dependency Install"
echo "====================================="
echo
echo "What would you like to install?"
echo "1) API dependencies"
echo "2) Web dependencies"
echo "3) Both"
echo "4) Exit"
echo

install_dotnet() {
    echo "Installing .NET SDK..."
    if [[ "$OSTYPE" == "msys" ]] || [[ "$OSTYPE" == "win32" ]]; then
        winget install Microsoft.DotNet.SDK.9
    else
        # Linux/macOS
        if [[ "$OSTYPE" == "darwin"* ]]; then
            brew install dotnet@9
        else
            # Linux
            wget https://dot.net/v1/dotnet-install.sh
            chmod +x dotnet-install.sh
            ./dotnet-install.sh --version latest
            rm dotnet-install.sh
        fi
    fi
}

install_mongodb() {
    echo "Installing MongoDB..."
    if [[ "$OSTYPE" == "msys" ]] || [[ "$OSTYPE" == "win32" ]]; then
        winget install MongoDB.Server
    else
        if [[ "$OSTYPE" == "darwin"* ]]; then
            brew tap mongodb/brew
            brew install mongodb-community@7.0
        else
            # Linux
            wget -qO - https://www.mongodb.org/static/pgp/server-7.0.asc | sudo apt-key add -
            echo "deb [ arch=amd64,arm64 ] https://repo.mongodb.org/apt/ubuntu focal/mongodb-org/7.0 multiverse" | sudo tee /etc/apt/sources.list.d/mongodb-org-7.0.list
            sudo apt-get update
            sudo apt-get install -y mongodb-org
        fi
    fi
}

install_deno() {
    echo "Installing Deno..."
    if [[ "$OSTYPE" == "msys" ]] || [[ "$OSTYPE" == "win32" ]]; then
        irm https://deno.land/install.ps1 | iex
    else
        curl -fsSL https://deno.land/install.sh | sh
    fi
}

install_api() {
    echo "📦 Installing API dependencies..."
    if ! install_dotnet; then
        echo "❌ .NET SDK installation failed!"
        return 1
    fi
    
    if ! install_mongodb; then
        echo "❌ MongoDB installation failed!"
        return 1
    fi

    # Install NuGet packages
    dotnet restore api/LMWebAPI/LMWebAPI.csproj
    
    echo "✅ API dependencies installed successfully!"
}

install_web() {
    echo "📦 Installing Web dependencies..."
    if ! install_deno; then
        echo "❌ Deno installation failed!"
        return 1
    fi
    
    # Install HTMX and AlpineJS via npm
    cd web && deno cache main.ts
    
    echo "✅ Web dependencies installed successfully!"
}

read -p "Enter your choice (1-4): " choice

case $choice in
    1)
        install_api
        ;;
    2)
        install_web
        ;;
    3)
        install_api && install_web
        ;;
    4)
        echo "Exiting..."
        exit 0
        ;;
    *)
        echo "Invalid choice. Please run the script again."
        exit 1
        ;;
esac

echo "🎉 Installation complete!"
