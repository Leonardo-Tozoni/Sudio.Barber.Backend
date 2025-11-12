using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Studio.Barber.Backend.Application.Interfaces;
using Studio.Barber.Backend.Application.Services;
using Studio.Barber.Backend.Domain.Interfaces;
using Studio.Barber.Backend.Infrastructure.Data;
using Studio.Barber.Backend.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Studio Barber API",
        Version = "v1",
        Description = "API para gerenciamento de barbeiros e barbearias",
        Contact = new OpenApiContact
        {
            Name = "Studio Barber",
            Email = "contato@studiobarber.com"
        }
    });
});

// DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? builder.Configuration["DATABASE_URL"] 
    ?? "postgresql://neondb_owner:npg_SzhyWxj25okF@ep-odd-queen-ac35k6xm-pooler.sa-east-1.aws.neon.tech/neondb";

// Converter URL do PostgreSQL para formato de connection string se necessário
if (connectionString.StartsWith("postgresql://"))
{
    var uri = new Uri(connectionString);
    var username = uri.UserInfo.Split(':')[0];
    var password = uri.UserInfo.Split(':').Length > 1 ? uri.UserInfo.Split(':')[1] : "";
    var host = uri.Host;
    var port = uri.Port == -1 ? 5432 : uri.Port;
    var database = uri.AbsolutePath.TrimStart('/');
    
    connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};SSL Mode=Require;";
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// Garantir que a coluna barbershopId permite NULL
builder.Services.AddHostedService<DatabaseMigrationService>();

// Injeção de Dependência
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBarberRepository, BarberRepository>();
builder.Services.AddScoped<IBarberService, BarberService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Studio Barber API V1");
        c.RoutePrefix = string.Empty; // Define Swagger como página inicial
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();