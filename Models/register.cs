

namespace register.Models;

public class UserRegister
{
    public Guid id { get; set; } = Guid.NewGuid();

    public string username { get; set; } = string.Empty;

    public int? NUPTK { get; set; }

    public string password { get; set; } = string.Empty;

    public string email { get; set; } = string.Empty;

    public string role { get; set; } = "user"; // default role
}
