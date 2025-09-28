using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace LMWebAPI.Models;

public class User_old
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }

    [BsonElement("username")]
    public required string Username { get; set; }

    [BsonElement("password")]
    public required string Password { get; set; }

    [BsonElement("first_name")]
    public required string FirstName { get; set; }

    [BsonElement("last_name")]
    public required string LastName { get; set; }

    [BsonElement("naf_details")]
    public required NAFInfo NafDetails { get; set; }

    [BsonElement("profile_image")]
    public string ProfileImage { get; set; } = "default_profile_image.png";

    [BsonElement("email")]
    public required string Email { get; set; }

    [BsonElement("email_confirmed")]
    public required bool EmailConfirmed { get; set; } = false;

    [BsonElement("login_suspended")]
    public required bool LoginSuspended { get; set; } = false;

    [BsonElement("login_suspension_expiry")]
    public required DateTime LoginSuspensionExpiry { get; set; } = DateTime.MinValue;

    [BsonElement("disabled")]
    public required bool Disabled { get; set; } = false;
}

public class NAFInfo
{
    [BsonElement("naf_number")]
    public required int NafNumber { get; set; } = 0;

    [BsonElement("naf_nickname")]
    public required string NafNickname { get; set; } = "";
}