using Microsoft.AspNetCore.Mvc;
using dto.logindto;


namespace MyMvcApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly service.loginservices.LoginService _loginService;

        public LoginController(service.loginservices.LoginService loginService)
        {
            _loginService = loginService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            try
            {
                await _loginService.loginauth(user);
                return RedirectToAction("Verify", "Register");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Terjadi kesalahan: " + ex.Message);
                return View(user);
            }
        }
    }
}
