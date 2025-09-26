
namespace dto.registerresponsedto;

public class RegisterResponse
{
 public class UserResponseDto
{
    public string Username { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public string Message { get; set; } = "";
}

}
