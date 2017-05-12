using HastaneRandevu.Models;
using HastaneRandevu.ViewModels;
using NHibernate.Linq;
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
        public ActionResult Login(AuthLogin form, string returnUrl)
        {
            //var user = Database.Session.Query<User>().FirstOrDefault(u => u.KimlikNo == form.KimlikNo);

            //if (user == null)
            //    HastaneRandevu.Models.User.FakeHash();

            //if (user == null || !user.CheckPassword(form.Password))
            //    ModelState.AddModelError("Kimlik No", "Kimlik No ya da Sifre yanlistir");
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            FormsAuthentication.SetAuthCookie(form.KimlikNo, true);

            if (!string.IsNullOrWhiteSpace(returnUrl))
                return Redirect(returnUrl);

            if (User.IsInRole("Admin"))
                return Content("Welcome Admin");
            else if (User.IsInRole("Doktor"))
                return Content("Welcome Doc");
            else
                return RedirectToAction("Index", "Home", new { area = "Hasta" });
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Login");
        }
    }
}