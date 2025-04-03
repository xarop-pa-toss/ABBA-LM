#!/bin/bash

echo "Welcome to ABBA League Manager dependency installer!"
echo "What would you like to install?"
echo "1) API dependencies"
echo "2) Web dependencies"
echo "3) Both"
echo "4) Exit"

read -p "Enter your choice (1-4): " choice

install_api() {
    echo "Installing API dependencies..."
    if [[ "$OSTYPE" == "msys" ]] || [[ "$OSTYPE" == "win32" ]]; then
        # Windows
        powershell -File ./api/requirements/install.ps1
    else
        # Linux/macOS
        cd api/requirements
        chmod +x install.sh
        ./install.sh
        cd ../..
    fi
    echo "✅ API dependencies installed!"
}

install_web() {
    echo "Installing Web dependencies..."
    cd web/requirements
    chmod +x install.sh
    ./install.sh
    cd ../..
    echo "✅ Web dependencies installed!"
}

case $choice in
    1)
        install_api
        ;;
    2)
        install_web
        ;;
    3)
        install_api
        install_web
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

echo "All requested dependencies installed successfully!"
