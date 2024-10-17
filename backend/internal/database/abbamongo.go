package database

import (
	"context"
	"log"
	"time"

	"github.com/spf13/viper"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
	"go.mongodb.org/mongo-driver/mongo/readpref"
)

type DBClient struct {
	Client *mongo.Client
}

func CreateDBClient() (*DBClient, context.Context) {
	viper.SetConfigFile("./config/.env")
	if err := viper.ReadInConfig(); err != nil {
		log.Fatal(err)
	}

	// Database connection
	ctx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
	client, err := mongo.Connect(options.Client().ApplyURI(viper.GetString("MONGO_URI")))
	if err != nil {
		log.Fatal(err)
	}

	// PING server to check if connection established
	ctxPing, cancel := context.WithTimeout(context.Background(), 10*time.Second)
	defer cancel()
	err = client.Ping(ctxPing, readpref.Primary())
	if err != nil {
		log.Fatal(err)
	}

	// Create Background Context
	ctx = context.Background()

	defer func() {
		if err = client.Disconnect(ctx); err != nil {
			panic(err)
		}
	}()

	return &DBClient{Client: client}, ctx
}

func (db *DBClient) GetEntry(collectionName string, filter interface{}, ctx context.Context) (interface{}, error) {
	collection := db.Client.Database("yourDatabase").Collection(collectionName)

	var result interface{}
	err := collection.FindOne(ctx, filter).Decode(&result)
	return result, err
}
