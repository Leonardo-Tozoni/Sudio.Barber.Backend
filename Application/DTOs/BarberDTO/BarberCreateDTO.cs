namespace Studio.Barber.Backend.Application.DTOs.BarberDTO;

public class BarberCreateDTO
{
    // Dados do usuário (para criar novo usuário)
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    
    // Se UserId for fornecido, usa usuário existente. Caso contrário, cria novo usuário
    public string? UserId { get; set; }
    
    // ID da barbearia (obrigatório)
    public string? BarbershopId { get; set; }
}

public class BarberDTO
{
    public string? Id { get; set; }
    public string? UserId { get; set; }
    public string? BarbershopId { get; set; }
    public DateTime CreatedAt { get; set; }
}

