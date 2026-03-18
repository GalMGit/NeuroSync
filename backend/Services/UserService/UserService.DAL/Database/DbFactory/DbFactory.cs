using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using UserService.CORE.Entities;

namespace UserService.DAL.Database.DbFactory;

public class DbFactory
{
    private readonly IMongoDatabase _database;

    public DbFactory(string? connectionString, string? databaseName)
    {
        var client = new MongoClient(connectionString);

        _database = client
            .GetDatabase(databaseName);

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
    }


    public IMongoCollection<User> GetUsersCollection()
        => _database.GetCollection<User>("users");
}
