using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("username")]
    public string Username { get; set; }

    [BsonElement("first_name")]
    public string FirstName { get; set; }

    [BsonElement("last_name")]
    public string LastName { get; set; }

    [BsonElement("password")]
    public string Password { get; set; }

    [BsonElement("password_salt")]
    public string PasswordSalt { get; set; }
}