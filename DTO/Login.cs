using System.ComponentModel.DataAnnotations;

namespace dto.logindto;

public class LoginRequest
{
    [Required(ErrorMessage = "Email harus diisi.")]
    [EmailAddress(ErrorMessage = "Format email tidak valid.")]
    [Display(Name = "Email")]
    [MaxLength(256, ErrorMessage = "Email maksimal 256 karakter")]
    public string email { get; set; } = string.Empty;
    [Required(ErrorMessage = "Password harus diisi.")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password minimal 8 karakter")]
    [Display(Name = "Password")]
    [MaxLength(256, ErrorMessage = "Password maksimal 256 karakter")]
    public string password { get; set; } = string.Empty;
}
