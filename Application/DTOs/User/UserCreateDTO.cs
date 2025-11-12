namespace Studio.Barber.Backend.Application.DTOs.User;

public class UserCreateDTO
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public bool IsBarber { get; set; }
}

public class UserEditDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
}

public class UserDTO
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public bool IsBarber { get; set; }
}