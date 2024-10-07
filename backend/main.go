package main

import (
	"fmt"
	"log"
	"net/http"

	"github.com/spf13/viper"
)

func main() {
	// Viper configuration loading
	viper.SetConfigFile("./config/.env")
	viper.ReadInConfig()

	fmt.Println((viper.Get("PORT")))

	fs := http.FileServer(http.Dir("../frontend/dist"))
	http.Handle("/", fs)

	log.Println("Listening on :8080...")
	err := http.ListenAndServe(":8080", nil)
	if err != nil {
		log.Fatal(err)
	}
}
