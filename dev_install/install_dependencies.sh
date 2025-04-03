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

install_api() {
    echo "📦 Installing API dependencies..."
    if [[ "$OSTYPE" == "msys" ]] || [[ "$OSTYPE" == "win32" ]]; then
        # Windows
        if ! powershell -File ./api/requirements/install.ps1; then
            echo "❌ API installation failed!"
            return 1
        fi
    else
        # Linux/macOS
        cd api/requirements || exit 1
        chmod +x install.sh
        if ! ./install.sh; then
            echo "❌ API installation failed!"
            cd ../..
            return 1
        fi
        cd ../..
    fi
    echo "✅ API dependencies installed successfully!"
}

install_web() {
    echo "📦 Installing Web dependencies..."
    cd web/requirements || exit 1
    chmod +x install.sh
    if ! ./install.sh; then
        echo "❌ Web installation failed!"
        cd ../..
        return 1
    fi
    cd ../..
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

echo "🎉 Installation complete!"
