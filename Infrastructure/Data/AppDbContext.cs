using Microsoft.EntityFrameworkCore;
using Studio.Barber.Backend.Domain.Entities;

namespace Studio.Barber.Backend.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }
    
    public DbSet<User> Users { get; set; }
}