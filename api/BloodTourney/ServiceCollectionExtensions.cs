using BloodTourney.Ruleset;
using BloodTourney.Tournament;
using Microsoft.Extensions.DependencyInjection;

namespace BloodTourney;

/// <summary>
/// Registration of services for Dependency Injection or regular calls
/// </summary>
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(IServiceCollection services)
    {
        services.AddScoped<ITournamentManager, TournamentManager>();
        services.AddScoped<IRulesetManager, RulesetManager>();
        
        return services;
    }
    
    public static IServiceCollection AddServicesAsSingleton(this IServiceCollection services)
    {
        services.AddSingleton<ITournamentManager, TournamentManager>();
        services.AddSingleton<IRulesetManager, RulesetManager>();
        
        return services;
    }
}