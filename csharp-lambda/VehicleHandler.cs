
namespace AwsDotnetCsharp;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json;

#nullable enable

[BsonIgnoreExtraElements]
public class Vehicle
{
    [BsonElement("make")]
    public string? Make { get; set; }
    [BsonElement("model")]
    public string? Model { get; set; }
    [BsonElement("year")]
    public int? Year { get; set; }

    public override string ToString()
    {
        return $"year: {Year}, make: {Make}, model: {Model}";
    }
}

public class VehicleHandler
{
    private static MongoClient? Client;
    private static MongoClient CreateMongoClient()
    {
        var mongoClientSettings = MongoClientSettings.FromConnectionString(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
        return new MongoClient(mongoClientSettings);
    }

    static VehicleHandler()
    {
        Client = CreateMongoClient();
    }

    public Response Vehicles(Request request)
    {
        string dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "";
        string collectionName = Environment.GetEnvironmentVariable("COLLECTION_NAME") ?? "";
        int documentLimit = Int32.Parse(s: (Environment.GetEnvironmentVariable("DOCUMENT_LIMIT") ?? "50"));

        if (Client != null)
        {
            try
            {
                var database = Client.GetDatabase(dbName);
                var collection = database.GetCollection<Vehicle>(collectionName);
                var documents = collection.Find(new BsonDocument()).Limit(documentLimit).ToList();
                return new Response(JsonSerializer.Serialize(documents), request);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response(ex.Message, request);
            }
        }
        else
        {
            return new Response("DB not initialized", request);
        }
    }
}
