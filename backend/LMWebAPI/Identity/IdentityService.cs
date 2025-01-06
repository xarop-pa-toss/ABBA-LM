using Microsoft.AspNetCore.Identity.Data;
using LMWebAPI.Repositories;
using LMWebAPI.Resources.Errors;
using MongoDB.Driver;
using BCrypt.Net;
using LMWebAPI.Models.DTOs;

namespace LMWebAPI.Identity;

public class IdentityService<T>(IMongoDatabase database) : MongoRepository<T>(database, "users")
{
    public async Task<IdentityService<T>> ValidateUserCredentials(LoginRequest loginRequest)
    {
        var filter = Builders<T>.Filter.Eq("email", loginRequest.Email);
        var projection = Builders<T>.Projection.Include("email");
        
        var user = await Collection.Find(filter).Project(projection).FirstOrDefaultAsync();
        if (user is not null)
        {
            var hashedPw = BCrypt.Net.BCrypt.HashPassword(loginRequest.Password);
            var storedPw = user.GetValue("password").ToString();
            
            if (!BCrypt.Net.BCrypt.Verify(hashedPw, storedPw))
            {
                throw new ProblemNotFoundException("Invalid username or password.");
            }
        }
        // MISSING RETURN
    }
}