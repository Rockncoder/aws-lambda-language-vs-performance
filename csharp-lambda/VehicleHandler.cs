
namespace AwsDotnetCsharp;

using MongoDB.Bson;
using MongoDB.Driver;
using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization;

#nullable enable

[BsonIgnoreExtraElements]
public class Vehicle
{
    public ObjectId Id { get; set; }
    [BsonElement("id")]
    public int? VehicleId { get; set; }
    [BsonElement("make")]
    public string? Make { get; set; }
    [BsonElement("model")]
    public string? Model { get; set; }
    [BsonElement("year")]
    public int? Year { get; set; }
    [BsonElement("UCity")]
    public double? CityMileage { get; set; }
    [BsonElement("UHighway")]
    public double? HighwayMileage { get; set; }
    [BsonElement("VClass")]
    public string? VehicleClass { get; set; }
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
        int documentLimit = Int32.Parse(Environment.GetEnvironmentVariable("COLLECTION_NAME") ?? "50");

        if (Client != null)
        {
            try
            {
                var database = Client.GetDatabase(dbName);
                var collection = database.GetCollection<Vehicle>(collectionName);
                var result = collection.Find<Vehicle>(FilterDefinition<Vehicle>.Empty)
                .Project(Builders<Vehicle>.Projection.Exclude(car => car.Id))
                .Limit(documentLimit).ToList();
                Console.WriteLine(result.ToString());
                return new Response(result.ToString(), request);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new Response("Handling failed", request);
            }
        }
        else
        {
            return new Response("DB not initialized", request);
        }
    }
}
