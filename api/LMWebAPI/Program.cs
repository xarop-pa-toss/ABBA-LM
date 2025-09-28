using BloodTourney.Ruleset;
using LMWebAPI.Data;
using LMWebAPI.Identity;
using LMWebAPI.Repositories;
using LMWebAPI.Resources.Errors;
using LMWebAPI.Services.Players;
using LMWebAPI.Services.Teams;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Scalar.AspNetCore;
using Serilog;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        #region Logging (Serilog)
        builder.Logging.ClearProviders();
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .WriteTo.Seq("http://localhost:5341")
            .CreateLogger();

        builder.Host.UseSerilog();

        Log.Information("Serilog logging initialized.");
        #endregion

        builder.Configuration.AddEnvironmentVariables();
        // Forces verification of DI on build instead of runtime
        builder.Host.UseDefaultServiceProvider((context, options) =>
            {
                options.ValidateScopes = true;
                options.ValidateOnBuild = true;
            }
        );

        #region Environment Setup
        // Load the appsettings.json and then override with environment-specific file
        builder.Configuration
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);

        if (builder.Environment.IsDevelopment())
            Log.Warning("Environment is set to PRODUCTION!");
        else
            Log.Information("Environment is set to: " + builder.Environment.EnvironmentName + ".");
        #endregion

        #region Database - Postgres on Fly.io
        builder.Services.AddDbContext<ApiDbContext>(options =>
            options.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")));
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

        #region --INACTIVE-- MongoDB connection/client
        builder.Services.AddSingleton<IMongoClient>(sp =>
        {
            Console.WriteLine(builder.Configuration["MONGO_CONNECTION_STRING_DEV"] + " - " + builder.Configuration["MONGO_DATABASE_NAME"]);
            var connectionString = builder.Configuration["MONGO_CONNECTION_STRING_DEV"];
            return new MongoClient(connectionString);
        });

        builder.Services.AddScoped<IMongoDatabase>(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            return client.GetDatabase(builder.Configuration["MONGO_DATABASE_NAME"]);
        });

        builder.Services.AddScoped(typeof(MongoRepository<>));
        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        #endregion

        #region Controllers / Services / Repositories
// Add Repositories
        builder.Services.AddScoped<PlayerRepository>();
// Add Services
        builder.Services.AddSingleton<JwtGenerator>();
        builder.Services.AddScoped<PlayerService>();
        builder.Services.AddScoped<PlayerSkillService>();
        builder.Services.AddScoped<TeamService>();
//From BloodTourney
        builder.Services.AddSingleton<RulesetManager>();
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

        #region Scalar (OpenAPI Documentation)
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

        Log.Information("Application started.");
        Log.Information("Scalar OpenAPI documentation available at http://localhost:5000/scalar/api-reference");
    }
}