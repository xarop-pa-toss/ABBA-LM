using Microsoft.AspNetCore.Identity.Data;
using LMWebAPI.Repositories;
using LMWebAPI.Resources.Errors;
using MongoDB.Driver;
using Crypt = BCrypt.Net.BCrypt;
using LMWebAPI.Models;

namespace LMWebAPI.Identity;

public class IdentityService : MongoRepository<User>
{
    public IdentityService(IMongoDatabase database, IMongoClient client) : base(database, "users", client)
    {
    }
    
    // public async Task<IdentityService> ValidateUserCredentials(LoginRequest loginRequest)
    // {
    //     var filter = Builders<User>.Filter.Eq("email", loginRequest.Email);
    //     var projection = Builders<User>.Projection.Include("email");
    //     
    //     var user = await Collection.Find(filter).Project(projection).FirstOrDefaultAsync();
    //     if (user is not null)
    //     {
    //         var hashedPw = Crypt.HashPassword(loginRequest.Password);
    //         var storedPw = user.GetValue("password").ToString();
    //         
    //         if (!Crypt.Verify(hashedPw, storedPw))
    //         {
    //             throw new ProblemNotFoundException("Invalid username or password.");
    //         }
    //     }
    //          // MISSING RETURN
    // }
}