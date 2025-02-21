using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace LMWebAPI.Models.DTOs;

public class CoachDTO
{
    public ObjectId Id { get; set; }
    public required string Username { get; set; }
}