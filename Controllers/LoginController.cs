using Microsoft.AspNetCore.Mvc;

namespace MyMvcApp.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
public IActionResult Check(string username, string password)
{
            if (username == "admin" && password == "123")
            {
                return RedirectToAction("Index", "Home"); // masuk ke Todo List
            }
            else
            {
                // Jika username atau password salah, tampilkan pesan error
                ModelState.AddModelError("", "Username atau Password salah");
                return View("login");
            }

}

    }
}
