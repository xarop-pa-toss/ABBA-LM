using Microsoft.AspNetCore.Identity.Data;
using LMWebAPI.Repositories;
using MongoDB.Driver;

namespace LMWebAPI.Identity;

public class IdentityService<T>(IMongoDatabase database) : MongoRepository<T>(database, "users")
{
    public IdentityService<T> ValidateUserCredentials(LoginRequest loginRequest)
    {
        var userDetails = new
        {
            ID = "",
            Email = loginRequest.Email,
            Password = loginRequest.Password,
        };
    }
}