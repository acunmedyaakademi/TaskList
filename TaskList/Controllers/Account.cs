using Microsoft.AspNetCore.Mvc;
using TaskList.Models;
using TaskList.Models.ViewModels.UserViewModels;

namespace TaskList.Controllers
{
    public class Account : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginUser loginUser)
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User user)
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ResetPassword model)
        {
            return View();
        }
    }
}
