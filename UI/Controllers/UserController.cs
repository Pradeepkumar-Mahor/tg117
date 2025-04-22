using Microsoft.AspNetCore.Mvc;
using UI.Models.User;

namespace UI.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            LogInViewModel logInViewModel = new LogInViewModel();
            return View(logInViewModel);
        }

        [HttpPost]
        public IActionResult Login(LogInViewModel logInViewModel)
        {
            return View();
        }

        public IActionResult ReSetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ReSetPassword(ReSetPasswordViewModel reSetPasswordViewModel)
        {
            return View();
        }

        public IActionResult NewUser()
        {
            RegisterViewModel registerViewModel = new();
            return View(registerViewModel);
        }

        [HttpPost]
        public IActionResult NewUser(RegisterViewModel registerViewModel)
        {
            return View();
        }
    }
}