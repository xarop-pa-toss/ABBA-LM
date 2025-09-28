using MongoDB.Bson.Serialization.Attributes;
namespace LMWebAPI.Models.DTOs;

public class LoginDTO
{
    [BsonElement("username")]
    public required string Username { get; set; }

    [BsonElement("password")]
    public required string Password { get; set; }
}