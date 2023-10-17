using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Udlånssystem_API.Data;
using System;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Udlånssystem API", Version = "v1" });
});

// Configuration for DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<UdlånsContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)))); // Assuming MySQL version is 8.0.29
// Registering the AutoMapper to use profiles from all assemblies (you may need to adjust this based on your project's structure).
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Register your repository here (Ensure the namespaces are correct).
builder.Services.AddScoped<IBrugerRepository, BrugerRepository>();


var app = builder.Build();

// Test database connection
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<UdlånsContext>();
        // This ensures that you can connect to the database and runs any pending migrations. 
        // You may remove it if you don't want to apply migrations here.
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