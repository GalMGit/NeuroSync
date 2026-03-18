using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using PostService.CORE.Entities;

namespace PostService.DAL.Database.DbFactory;

public class DbFactory
{
    private readonly IMongoDatabase _database;

    public DbFactory(string? connectionString, string? databaseName)
    {
        var client = new MongoClient(connectionString);

        _database = client
            .GetDatabase(databaseName);

        BsonSerializer
            .RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
    }

    public IMongoCollection<Post> GetPostCollection(string collectionName)
        => _database.GetCollection<Post>(collectionName);
}
