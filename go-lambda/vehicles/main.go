package main

import (
	"context"
	"os"
	"strconv"

	"github.com/aws/aws-lambda-go/lambda"
	"go.mongodb.org/mongo-driver/bson"
	"go.mongodb.org/mongo-driver/bson/primitive"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

type EventInput struct {
	Limit int64 `json:"limit"`
}

type Vehicle struct {
	ID    primitive.ObjectID `bson:"_id" json:"_id"`
	Make  string             `bson:"make" json:"make"`
	Model string             `bson:"model" json:"model"`
	Year  int32              `bson:"year" json:"year"`
}

var client, err = mongo.Connect(context.Background(), options.Client().ApplyURI(os.Getenv("CONNECTION_STRING")))
var document_limit = os.Getenv("DOCUMENT_LIMIT")

func HandleRequest(ctx context.Context, input EventInput) ([]Vehicle, error) {
	if err != nil {
		return nil, err
	}

	maxDocuments, err := strconv.ParseInt(document_limit, 0, 64)
	collection := client.Database(os.Getenv("DB_NAME")).Collection(os.Getenv("COLLECTION_NAME"))

	opts := options.Find().SetLimit(maxDocuments)

	if input.Limit != 0 {
		opts = opts.SetLimit(input.Limit)
	}
	cursor, err := collection.Find(context.Background(), bson.M{}, opts)
	if err != nil {
		return nil, err
	}
	var vehicles []Vehicle
	if err = cursor.All(context.Background(), &vehicles); err != nil {
		return nil, err
	}

	return vehicles, nil
}

func main() {
	lambda.Start(HandleRequest)
}
