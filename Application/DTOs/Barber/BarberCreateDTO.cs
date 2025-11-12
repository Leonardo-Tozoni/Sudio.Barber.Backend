namespace Studio.Barber.Backend.Application.DTOs.Barber;

public class BarberCreateDTO
{
    public string? UserId { get; set; }
    public string? BarbershopId { get; set; }
}

public class BarberDTO
{
    public string? Id { get; set; }
    public string? UserId { get; set; }
    public string? BarbershopId { get; set; }
    public DateTime CreatedAt { get; set; }
}

