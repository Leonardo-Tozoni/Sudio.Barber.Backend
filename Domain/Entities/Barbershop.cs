namespace Studio.Barber.Backend.Domain.Entities;

public class Barbershop
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string? name { get; set; }
    public string? address { get; set; }
    public string? imageUrl { get; set; }
    public ICollection<Domain.Entities.Barber> barbers { get; set; } = new List<Domain.Entities.Barber>();
}

