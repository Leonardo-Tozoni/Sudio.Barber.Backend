namespace Studio.Barber.Backend.Domain.Entities;

public class User
{
    public string id { get; set; } = Guid.NewGuid().ToString();
    public string? name { get; set; }
    public string? email { get; set; }
    public string? role { get; set; }
    public DateTime? emailVerified { get; set; }
    public string? image { get; set; }
    public string? phone { get; set; }
    public bool? cookieConsent { get; set; }
    public DateTime? cookieConsentDate { get; set; }
    // Navigation properties
    public Domain.Entities.Barber? barber { get; set; }
}