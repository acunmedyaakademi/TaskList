using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TaskList.Business.Abstract;
using TaskList.Core;
using TaskList.Interfaces;
using TaskList.Models;
using TaskList.Models.ViewModels.UserViewModels;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace TaskList.Controllers
{
    public class AccountController : Controller
    {
        readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginUser loginUser)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Login = "Kullanıcı Adı veya Şifre Yanlış";
                return View();
            }
            SessionModel? model = _userService.Login(loginUser);
            if (model != null)
            {
                if (!model.MailConfirmed)
                {
                    return Content("Confirm Mail !");
                }
                HttpContext.Session.SetString("LoginId", model.Id.ToString());
                HttpContext.Session.SetString("LoginName", model.Name);
                HttpContext.Session.SetString("LoginMail", model.Email);
                return RedirectToAction("index", "home");
            }

            ViewBag.Login = "Kullanıcı Adı veya Şifre Yanlış";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AddUser user)
        {
            //todo recaptcha
           
            if (ModelState.IsValid)
            {
                //todo bussines bitince mail burada atılıp modele işlenecek

                if (_userService.Register(user))
                {
                    return RedirectToAction("login", "account");

                }
            }
            ViewBag.Register = "Kullanıcı eklenemedi";
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
            model.Email = HttpContext.Session.GetString("ForgetMail");

            if (ModelState.IsValid)
            {              
                if (_userService.ResetPassword(model))
                {
                    //todo burada login yapılabilir 

                    return RedirectToAction("login", "account");
                }
            }
            ViewBag.Reset = "Şifre değiştirilemedi, lütfen tekrar deneyiniz";
            return View();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgetPassword(string Email)
        {
            //todo recaptcha
            if (_userService.SendMailCode(Email))
            {
                HttpContext.Session.SetString("ForgetMail",Email);
                return RedirectToAction("ResetPassword", "Account");
            }
            
            ViewBag.Reset = "Mail gonderilemedi!";
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Remove("LoginId");
            HttpContext.Session.Remove("LoginName");
            HttpContext.Session.Remove("LoginMail");
            return RedirectToAction("login", "account");

        }
    }
}

