using LMWebAPI.Models;
using LMWebAPI.Repositories;
using MongoDB.Driver;
namespace LMWebAPI.Identity;

public class IdentityService : MongoRepository<User_old>
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