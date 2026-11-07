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
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IStatusRepository, StatusRepository>();
        return services;
    }
}