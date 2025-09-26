using System.ComponentModel.DataAnnotations;


namespace dto.registerrequestdto;

public class Registerdto
{

    [Required(ErrorMessage = "Email harus diisi.")]
    [EmailAddress(ErrorMessage = "Format email tidak valid.")]
    [Display(Name = "Email")]
    [MaxLength(256, ErrorMessage = "Email maksimal 256 karakter")]
    public string email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Username harus diisi.")]
    [MinLength(5, ErrorMessage = "Username minimal 5 karakter")]
    [Display(Name = "Username")]
    [MaxLength(256, ErrorMessage = "Username maksimal 50 karakter")]
    public string username { get; set; } = string.Empty;
    [Range(1000000000000000, 9999999999999999, ErrorMessage = "NUPTK harus 16 digit angka")]
    [Display(Name = "NUPTK")]
    public int? NUPTK { get; set; }
    [Required(ErrorMessage = "Password harus diisi.")]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage = "Password minimal 8 karakter")]
    [Display(Name = "Password")]
    public string password { get; set; } = string.Empty;
    [Required(ErrorMessage = "Konfirmasi password harus diisi.")]
    [DataType(DataType.Password)]
    [Compare("password", ErrorMessage = "Password dan konfirmasi password tidak sama")] // Huruf kecil 'p'
    [Display(Name = "Confirm Password")]
    public string confirmPassword { get; set; } = string.Empty;

}
