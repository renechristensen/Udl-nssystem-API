using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Udl�nssystem_API.Data;
using System;
using Microsoft.OpenApi.Models;
using Udl�nssystem_API.Repositories.Implementations;
using Udl�nssystem_API.Repositories.Interfaces;
using Udl�nssystem_API.Services.Implementations;
using Udl�nssystem_API.Services.Interfaces;
using Udl�nssystem_API.Repositories;
using Udl�nssystem_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Udl�nssystem API", Version = "v1" });
});

// Configuration for DbContext
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<Udl�nsContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29))));

// Registering the AutoMapper to use profiles from all assemblies
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register repositories
builder.Services.AddScoped<IBrugerRepository, BrugerRepository>();
builder.Services.AddScoped<IBrugerGruppeRepository, BrugerGruppeRepository>();
builder.Services.AddScoped<IStamklasseRepository, StamklasseRepository>();
builder.Services.AddScoped<IPostnrRepository, PostnrRepository>();
builder.Services.AddScoped<IComputerRepository, ComputerRepository>();
builder.Services.AddScoped<IUdl�nRepository, Udl�nRepository>();



// Register Services
builder.Services.AddScoped<IBrugerGruppeService, BrugerGruppeService>();
builder.Services.AddScoped<IStamklasseService, StamklasseService>();
builder.Services.AddScoped<IPostnrService, PostnrService>();
builder.Services.AddScoped<IComputerService, ComputerService>();
builder.Services.AddScoped<IUdl�nService, Udl�nService>();


var app = builder.Build();

// Test database connection
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<Udl�nsContext>();
        context.Database.Migrate();
        Console.WriteLine("Connected to the database successfully.");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while connecting to the database.");
        Console.WriteLine("An error occurred while connecting to the database: " + ex.Message);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Udl�nssystem API v1"));
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();