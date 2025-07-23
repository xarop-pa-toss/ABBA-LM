using BloodTourney.Ruleset;
using LMWebAPI.Identity;
using LMWebAPI.Repositories;
using LMWebAPI.Resources.Errors;
using LMWebAPI.Services.Players;
using LMWebAPI.Services.Teams;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.Features;
using MongoDB.Driver;
using Scalar.AspNetCore;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddEnvironmentVariables();
        // Forces verification of DI on build instead of runtime
        builder.Host.UseDefaultServiceProvider((context, options) =>
            {
                options.ValidateScopes = true;
                options.ValidateOnBuild = true;
            }
        );

        #region Environment
        // Load the appsettings.json and then override with environment-specific file
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        #endregion
        
        #region PostgreSQL connection/client
        // CREATE-USE-CLOSE A CONNECTION WHENEVER AND WHEREVER IT IS NEEDED
        // Example:
        // public class ExampleService
        // {
        //     public ExampleService(IConfiguration config)
        //     {
        //         var connStr = config.GetConnectionString("DefaultConnection");
        //     }
        //     using var connection = new NpgsqlConnection(connectionString);
        //     connection.Open()
        // }
        #endregion
        
// This solution uses User Secrets, a .NET feature that can be created with "dotnet user-secrets init".
// It only works in development environment which is set with "set ASPNETCORE_ENVIRONMENT=Development".
        // #region MongoDB connection/client
        // builder.Services.AddSingleton<IMongoClient>(sp =>
        // {
        //     Console.WriteLine(builder.Configuration["MONGO_CONNECTION_STRING_DEV"] + " - " + builder.Configuration["MONGO_DATABASE_NAME"]);
        //     var connectionString = builder.Configuration["MONGO_CONNECTION_STRING_DEV"];
        //     return new MongoClient(connectionString);
        // });
        //
        // builder.Services.AddScoped<IMongoDatabase>(sp =>
        // {
        //     var client = sp.GetRequiredService<IMongoClient>();
        //     return client.GetDatabase(builder.Configuration["MONGO_DATABASE_NAME"]);
        // });
        //
        // builder.Services.AddScoped(typeof(MongoRepository<>));
        // builder.Services.AddRouting(options => options.LowercaseUrls = true);
        // #endregion

        #region Controllers / Services / Repositories
// Add Repositories
        builder.Services.AddScoped<PlayerRepository>();

// Add Services
//From BloodTourney
        builder.Services.AddSingleton<RulesetManager>();
//

        builder.Services.AddSingleton<JwtGenerator>();
        builder.Services.AddScoped<PlayerService>();
        builder.Services.AddScoped<PlayerSkillService>();
        builder.Services.AddScoped<TeamService>();
        #endregion
        builder.Services.AddControllers();

        #region Middleware
// Add Global Error Handling
        builder.Services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance =
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
            };
        });
        builder.Services.AddExceptionHandler<ProblemExceptionHandler>();
        builder.Services.AddOpenApi();

// JWT
// builder.Services.AddAuthentication(options =>
//     {
//         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     })
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuer = true,
//             ValidateAudience = true,
//             ValidateLifetime = true,
//             ValidateIssuerSigningKey = true, 
//             ValidIssuer = builder.Configuration["Jwt:Issuer"],
//             ValidAudience = builder.Configuration["Jwt:Audience"],
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
//         };
//     });
// builder.Services.AddAuthorization();
        #endregion

        var app = builder.Build();

        #region  Scalar (OpenAPI Documentation)
        app.MapOpenApi();
        if (app.Environment.IsDevelopment())
        {
            app.MapScalarApiReference();
        }
        #endregion

        app.UseExceptionHandler();
        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseAuthentication();

        app.MapControllers();

        app.Run();
    }
}