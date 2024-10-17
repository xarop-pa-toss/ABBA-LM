package main

import (
	"context"
	"fmt"
	"log"
	"net/http"
	"time"

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

	// TEST MONGODB QUERY
	filter := bson.M{"name": "block"}
	ctx, cancel := context.WithTimeout(ctxBackground, 3*time.Second)
	defer cancel() // Ensures Cancel is called to free resources

	entry, err := dbClient.GetEntry("skills", filter, ctx)
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
