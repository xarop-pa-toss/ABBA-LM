package main

import (
	"context"
	"fmt"
	"log"
	"net/http"
	"time"
	"mux"

	"github.com/spf13/viper"
	abbamongo "github.com/xarop-pa-toss/ABBA-LM/backend/internal/database"
	"go.mongodb.org/mongo-driver/bson"
)

func main() {
	// Viper configuration loading
	viper.SetConfigFile("./config/.env")
	viper.ReadInConfig()
	port := viper.GetString("PORT")

	dbClient, ctxBackground := abbamongo.CreateDBClient()
	defer dbClient.Client.Disconnect(ctxBackground)

	log.Println("Listening on http://localhost:" + port)
	err := http.ListenAndServe(`:{port}`, nil)
	if err != nil {
		log.Fatal(err)
	}

	// ROUTES
	fs := http.FileServer(http.Dir("../frontend/dist"))
	http.Handle("/", fs)

	router := http.NewServeMux()
	router.HandleFunc("/skills", skills)
	
	func skills (response http.ResponseWriter, request *http.Request) {

	}
}
