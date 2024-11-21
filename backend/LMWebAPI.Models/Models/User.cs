using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace LMWebAPI.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("username")]
    public required string Username { get; set; }

    [BsonElement("first_name")]
    public required string FirstName { get; set; }

    [BsonElement("last_name")]
    public required string LastName { get; set; }

    [BsonElement("password")]
    public required string Password { get; set; }

    [BsonElement("password_salt")]
    public required string PasswordSalt { get; set; }
}