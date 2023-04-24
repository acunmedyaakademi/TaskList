using MessagePack.Resolvers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Reflection;
using TaskList.Business.Abstract;
using TaskList.Core;
using TaskList.Interfaces;
using TaskList.Models;
using TaskList.Models.ViewModels;
using TaskList.Models.ViewModels.UserViewModels;

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
           if(HttpContext.Session.GetString("LoginName") != null)
                return RedirectToAction("Index","home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginUser loginUser)
        {
            if (HttpContext.Session.GetString("LoginName") != null)
                return RedirectToAction("Index", "home");

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
                    return RedirectToAction("ConfirmMail", "account");//todo burayı düzeltt
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
            if (HttpContext.Session.GetString("LoginName") != null)
                return RedirectToAction("Index", "home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AddUser user)
        {
            if (HttpContext.Session.GetString("LoginName") != null)
                return RedirectToAction("Index", "home");
            //todo recaptcha

            if (ModelState.IsValid)
            {
                ResponseModel response = _userService.Register(user);

                if (response.Success)
                {
                    HttpContext.Session.SetString("ConfirmMail",user.Email);
                    return RedirectToAction("ConfirmMail", "account");

                }

                ViewBag.Register = response.Message;
                return View();
            }

            ViewBag.Register = "model valid değil";
            return View();
        }

        public IActionResult ResetPassword()
        {
            if (HttpContext.Session.GetString("LoginName") != null)
                return RedirectToAction("Index", "home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ResetPassword model)
        {
            if (HttpContext.Session.GetString("LoginName") != null)
                return RedirectToAction("Index", "home");
            model.Email = HttpContext.Session.GetString("ForgetMail");

            if (ModelState.IsValid)
            {
                ResponseModel response = _userService.ResetPassword(model);

                if (response.Success)
                {
                    //todo burada login yapılabilir 

                    return RedirectToAction("login", "account");
                }
                ViewBag.Reset = response.Message;
                return View();
            }
            ViewBag.Reset = "model valid değil";
            return View();
        }

        public IActionResult ForgetPassword()
        {
            if (HttpContext.Session.GetString("LoginName") != null)
                return RedirectToAction("Index", "home");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgetPassword(string Email)
        {
            if (HttpContext.Session.GetString("LoginName") != null)
                return RedirectToAction("Index", "home");

            if (ModelState.IsValid)
            {
                ResponseModel response = _userService.SendMailCode(Email);
                //todo recaptcha
                if (response.Success)
                {
                    HttpContext.Session.SetString("ForgetMail", Email);
                    return RedirectToAction("ResetPassword", "Account");
                }

                ViewBag.Forget = response.Message;
                return View();
            }
            ViewBag.Forget = "model valid değil";
            return View();
        }


        public IActionResult Logout()
        {

            HttpContext.Session.Remove("LoginId");
            HttpContext.Session.Remove("LoginName");
            HttpContext.Session.Remove("LoginMail");
            return RedirectToAction("login", "account");
        }

        public IActionResult ConfirmMail()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmMail(string mailCode)
        {
            string mail = HttpContext.Session.GetString("ConfirmMail");
            if (_userService.ConfirmMail(mail, mailCode))
            {
                return RedirectToAction("login","account");
            }
            return View();
        }
    }
}

