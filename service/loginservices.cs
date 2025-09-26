using MyApp.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace service.loginservices;

public class LoginService
{
    private readonly UserRepository _UserRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LoginService(UserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _UserRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task loginauth(dto.logindto.LoginRequest logindto)
    {
        var user = _UserRepository.GetLogin(logindto.email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(logindto.password, user.password))
        {
            throw new Exception("Email atau password salah.");
        }

        var claims = new List<System.Security.Claims.Claim>
        {
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.email),
            new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, user.role)
        };

        var claimsIdentity = new System.Security.Claims.ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true, // Simpan cookie meskipun browser ditutup
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Sesuaikan dengan ExpireTimeSpan di Program.cs
        };

        // 🔹 HttpContext diambil dari IHttpContextAccessor
        var httpContext = _httpContextAccessor.HttpContext!;
        await httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new System.Security.Claims.ClaimsPrincipal(claimsIdentity),
            authProperties
        );
    }
}
