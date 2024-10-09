package main

import (
	"context"
	"fmt"
	"log"
	"net/http"
	"time"

	"github.com/xarop-pa-toss/ABBA-LM/backend/cmd/internal/abbamongo"

	"github.com/spf13/viper"
	"go.mongodb.org/mongo-driver/v2/bson"
)

func main() {
	// Viper configuration loading
	viper.SetConfigFile("./config/.env")
	viper.ReadInConfig()
	port := viper.GetString("PORT")

	dbClient, ctxBackground := abbamongo.CreateDbClient()
	defer dbClient.Disconnect()

	// TEST MONGODB QUERY
	filter := bson.M{"name": "block"}
	entry, err := dbClient.GetEntry("skills", filter, context.WithTimeout(ctxBackground, 3*time.Second))
	if err != nil {
		log.Fatal(err)
	}
	fmt.Println(entry)

	fs := http.FileServer(http.Dir("../frontend/dist"))
	http.Handle("/", fs)

	log.Println("Listening on http://localhost:" + port)
	err = http.ListenAndServe(":"+port, nil)
	if err != nil {
		log.Fatal(err)
	}
}
