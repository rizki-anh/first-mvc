using Microsoft.AspNetCore.Mvc;
using dto.registerrequestdto;
using service.registerservice;

namespace MyMvcApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RegisterService _registerService;
        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Verify()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Registerdto user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            try
            {
                _registerService.RegisterUser(user);
                TempData["RegistrationSuccess"] = "Registrasi berhasil! Silakan login.";
                return RedirectToAction("Login", "Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Terjadi kesalahan: " + ex.Message);
                return View(user);
            }
        }
    }
}
