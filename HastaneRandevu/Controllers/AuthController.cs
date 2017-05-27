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
        public ActionResult Login(AuthLogin form)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(u => u.KimlikNo == form.KimlikNo);

            if (user == null)
                HastaneRandevu.Models.User.FakeHash();

            if (user == null || !user.CheckPassword(form.Password))
                ModelState.AddModelError("Kimlik No", "Kimlik No ya da Sifre yanlistir");
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            //bool redirect = false;
            //if (Auth.User != null)
            //    redirect = true;
            FormsAuthentication.SetAuthCookie(form.KimlikNo, true);

            //if (redirect)
            //    if (!string.IsNullOrWhiteSpace(returnUrl))
            //        return Redirect(returnUrl);

            if (User.IsInRole("Admin"))
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            else if (User.IsInRole("Doktor"))
                return RedirectToAction("Index", "Home", new { area = "Doktor" });
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