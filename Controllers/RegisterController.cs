using Microsoft.AspNetCore.Mvc;
using dto.registerrequestdto;
using service.registerservice;
using service.verify;
namespace MyMvcApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RegisterService _registerService;
        private readonly Verification _verificationService;
        public RegisterController(RegisterService registerService, Verification verificationService)
        {
            _registerService = registerService;
            _verificationService = verificationService;

        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
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
