using LMWebAPI.Models;
using LMWebAPI.Repositories;
using LMWebAPI.Services.Players;
using MongoDB.Driver;
var builder = WebApplication.CreateBuilder(args);

#region Add MongoDB connection/client
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDB");
    return new MongoClient(connectionString);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("DatabaseName");
});
#endregion

builder.Services.AddControllers();
builder.Services.AddOpenApi();

#region Controllers / Services / Repositories
// Add Repositories
builder.Services.AddScoped<PlayerRepository<Player>>();

// Add Services
builder.Services.AddScoped<PlayerService>();
builder.Services.AddScoped<PlayerSkillService>();
builder.Services.AddScoped<TeamService>();

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