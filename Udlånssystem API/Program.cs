using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Udlånssystem_API.Data;
using System;
using Microsoft.OpenApi.Models;
using Udlånssystem_API.Repositories.Implementations;
using Udlånssystem_API.Repositories.Interfaces;
using Udlånssystem_API.Services.Implementations;
using Udlånssystem_API.Services.Interfaces;
using Udlånssystem_API.Repositories;
using Udlånssystem_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Udlånssystem API", Version = "v1" });
});

// Configuration for DbContext
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UdlånsContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29))));

// Registering the AutoMapper to use profiles from all assemblies
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register repositories
builder.Services.AddScoped<IBrugerRepository, BrugerRepository>();
builder.Services.AddScoped<IBrugerGruppeRepository, BrugerGruppeRepository>();
builder.Services.AddScoped<IStamklasseRepository, StamklasseRepository>();
builder.Services.AddScoped<IPostnrRepository, PostnrRepository>();
builder.Services.AddScoped<IComputerRepository, ComputerRepository>();
builder.Services.AddScoped<IUdlånRepository, UdlånRepository>();



// Register Services
builder.Services.AddScoped<IBrugerGruppeService, BrugerGruppeService>();
builder.Services.AddScoped<IStamklasseService, StamklasseService>();
builder.Services.AddScoped<IPostnrService, PostnrService>();
builder.Services.AddScoped<IComputerService, ComputerService>();
builder.Services.AddScoped<IUdlånService, UdlånService>();


var app = builder.Build();

// Test database connection
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<UdlånsContext>();
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
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Udlånssystem API v1"));
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();