using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using PokemonGame.Entities;
using PokemonGame.Repos;
using PokemonGame.Seeders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PokemonDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("PokemonDB")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Seeder, LocationSeeder>();
builder.Services.AddScoped<Seeder, ItemsSeeder>();
builder.Services.AddScoped<Seeder, TypesSeeder>();
builder.Services.AddScoped<Seeder, StatusesSeeder>();
builder.Services.AddScoped<Seeder, AttacksSeeder>();
builder.Services.AddScoped<Seeder, PokemonSpeciesSeeder>();
builder.Services.AddScoped<Seeder, EvolutionSeeder>();
builder.Services.AddScoped<DatabaseSeeder>();

builder.Services.AddScoped<IItemRepo, ItemRepo>();
builder.Services.AddScoped<IStatusRepo, StatusRepo>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<PokemonDbContext>();

    await dbContext.Database.MigrateAsync();

    var seeder = scope.ServiceProvider
        .GetRequiredService<DatabaseSeeder>();

    await seeder.Seed();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
