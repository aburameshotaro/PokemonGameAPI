using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using PokemonGame.Domain.Interfaces;
using PokemonGame.Infrastructure.Persistence;
using PokemonGame.Infrastructure.Persistence.Repositories;
namespace PokemonGame.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PokemonDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("PokemonDB"),
            x => x.MigrationsAssembly(typeof(PokemonDbContext).Assembly.FullName)));
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();
        services.AddScoped<IPokemonSpeciesRepository, PokemonSpeciesRepository>();
        services.AddScoped<IAttackRepository, AttackRepository>();
        services.AddScoped<IPokemonRepository, PokemonRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}