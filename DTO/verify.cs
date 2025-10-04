
namespace entity.verify;

public class Verify
{
    public string email { get; set; } = string.Empty;
    public string code { get; set; } = string.Empty;
    public DateTime code_expire_time { get; set; }
}
