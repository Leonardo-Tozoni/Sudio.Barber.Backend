namespace Studio.Barber.Backend.Application.DTOs.User;

public class UserCreateDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsBarber { get; set; }
}

public class UserEditDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}

public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsBarber { get; set; }
}