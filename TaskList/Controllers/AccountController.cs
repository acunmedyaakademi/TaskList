﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TaskList.Interfaces;
using TaskList.Models;
using TaskList.Models.ViewModels.UserViewModels;

namespace TaskList.Controllers
{
    public class AccountController : Controller
    {
        readonly IUserDal _userDal;

        public AccountController(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginUser loginUser)
        {
            SessionModel? model = _userDal.Login(loginUser);
            if (model != null)
            {
                if (!model.MailConfirmed)
                {
                    return Content("Mailconfiret");
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

                if (_userDal.AddUser(user))
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
            //todo recaptcha

            if (ModelState.IsValid)
            {
                //todo bussines bitince mail burada atılıp modele işlenecek

                if (_userDal.ResetPassword(model))
                {
                    //todo burada login yapılabilir 

                    return RedirectToAction("login", "account");

                }
            }
            ViewBag.Reset = "Şifre değiştirilemedi, lütfen tekrar deneyiniz";
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("LoginId");
            HttpContext.Session.Remove("LoginName");
            HttpContext.Session.Remove("LoginMail");
            return View();
        }
    }
}
