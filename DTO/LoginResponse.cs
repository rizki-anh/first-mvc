namespace dto.loginresponsedto;

public class LoginResponse
{
    public string Username { get; set; } = "";
    public DateTime dateTime { get; set; } = DateTime.Now;
    public string Message { get; set; } = "";
}
