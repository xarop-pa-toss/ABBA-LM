package main

import (
	"flag"
	"log"
	"net/http"

	"github.com/spf13/viper"
	"github.com/xarop-pa-toss/ABBA-LM/backend/cmd/server"
	mongodb "github.com/xarop-pa-toss/ABBA-LM/backend/cmd/storage"
)

func main() {
	// Viper configuration loading
	viper.SetConfigFile("./config/.env")
	viper.ReadInConfig()

	// FLAGS
	listenAddr := flag.String("addr", ":3000", "Server address with port")
	flag.Parse()

	dbClient, ctxBackground := mongodb.CreateDBClient()
	defer dbClient.Client.Disconnect(ctxBackground)

	// START SERVER
	server := server.NewServer(*listenAddr)
	log.Println("Listening on ", *listenAddr)
	log.Fatal(server.Start())

	fs := http.FileServer(http.Dir("../frontend/dist"))
	http.Handle("/", fs)
}
