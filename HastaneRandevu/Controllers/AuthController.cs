using HastaneRandevu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HastaneRandevu.Controllers
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Login()
        {
            return View(new AuthLogin());
        }
        [HttpPost]
        public ActionResult Login(AuthLogin form)
        {
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            FormsAuthentication.SetAuthCookie(form.KimlikNo, true);
            if (User.IsInRole("hasta"))
                return RedirectToRoute("Home");
            return Content("Giris yaptim ama hasta degilsin");
        }
    }
}