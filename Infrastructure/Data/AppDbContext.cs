using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Studio.Barber.Backend.Domain.Entities;
using BarberEntity = Studio.Barber.Backend.Domain.Entities.Barber;

namespace Studio.Barber.Backend.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<BarberEntity> Barbers { get; set; }
    public DbSet<Barbershop> Barbershops { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configurar o enum do PostgreSQL
        modelBuilder.HasPostgresEnum("UserRole", new[] { "CLIENT", "BARBER" });
        
        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.name).HasColumnName("name");
            entity.Property(e => e.email).HasColumnName("email");
            entity.Property(e => e.role)
                .HasColumnName("role")
                .HasColumnType("UserRole")
                .HasConversion(
                    v => v != null ? v.ToUpper() : null,
                    v => v);
            entity.Property(e => e.emailVerified).HasColumnName("emailVerified");
            entity.Property(e => e.image).HasColumnName("image");
            entity.Property(e => e.phone).HasColumnName("phone");
            entity.Property(e => e.cookieConsent).HasColumnName("cookieConsent");
            entity.Property(e => e.cookieConsentDate).HasColumnName("cookieConsentDate");
            entity.HasIndex(e => e.email).IsUnique();
        });
        
        // Barber configuration
        modelBuilder.Entity<BarberEntity>(entity =>
        {
            entity.ToTable("Barber");
            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.userId).HasColumnName("userId");
            entity.Property(e => e.barbershopId).HasColumnName("barbershopId");
            entity.Property(e => e.createdAt).HasColumnName("createdAt");
            
            entity.HasIndex(e => e.userId).IsUnique();
            
            entity.HasOne(b => b.user)
                .WithOne(u => u.barber)
                .HasForeignKey<BarberEntity>(b => b.userId)
                .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(b => b.barbershop)
                .WithMany(bs => bs.barbers)
                .HasForeignKey(b => b.barbershopId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);
        });
        
        // Barbershop configuration
        modelBuilder.Entity<Barbershop>(entity =>
        {
            entity.ToTable("Barbershop");
            entity.HasKey(e => e.id);
            entity.Property(e => e.id).HasColumnName("id");
            entity.Property(e => e.name).HasColumnName("name");
            entity.Property(e => e.address).HasColumnName("address");
            entity.Property(e => e.imageUrl).HasColumnName("imageUrl");
        });
    }
}