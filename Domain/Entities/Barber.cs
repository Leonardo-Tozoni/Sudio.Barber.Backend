namespace Studio.Barber.Backend.Domain.Entities;

public class Barber
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string? userId { get; set; }
    public string? barbershopId { get; set; }
    public DateTime createdAt { get; set; } = DateTime.UtcNow;
    
    // Navigation properties
    public User? user { get; set; }
    public Barbershop? barbershop { get; set; }
}

