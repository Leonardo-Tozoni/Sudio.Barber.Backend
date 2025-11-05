using Microsoft.EntityFrameworkCore;
using Studio.Barber.Backend.Application.Interfaces;
using Studio.Barber.Backend.Application.Services;
using Studio.Barber.Backend.Domain.Interfaces;
using Studio.Barber.Backend.Infrastructure.Data;
using Studio.Barber.Backend.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção de Dependência
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();