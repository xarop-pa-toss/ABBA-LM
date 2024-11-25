using LMWebAPI.Models;
using LMWebAPI.Repositories;
using LMWebAPI.Services.Players;
using LMWebAPI.Services.Teams;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// This solution uses User Secrets, a .NET feature that can be created with "dotnet user-secrets init".
// It only works in development environment which is set with "set ASPNETCORE_ENVIRONMENT=Development".
#region MongoDB connection/client
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration["MONGO_CONNECTION_STRING_DEV"];
    return new MongoClient(connectionString);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("MONGO_DATABASE_NAME");
});
#endregion

#region Controllers / Services / Repositories
// Add Repositories
builder.Services.AddScoped<PlayerRepository<Player>>();

// Add Services
builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<PlayerSkillService>();
builder.Services.AddScoped<TeamService>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

var app = builder.Build();

#region Environment settings
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();   
}
#endregion

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();