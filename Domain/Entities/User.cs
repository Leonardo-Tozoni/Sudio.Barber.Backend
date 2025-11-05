namespace Studio.Barber.Backend.Domain.Entities;

public class User
{
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string role { get; set; }
    public DateTime createdAt { get; set; }
    public bool IsBarber { get; set; }
}