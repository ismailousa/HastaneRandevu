using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HastaneRandevu.Infrastructure;
using HastaneRandevu.Models;
using HastaneRandevu.Areas.Hasta.ViewModels;

namespace HastaneRandevu.Areas.Hasta.Controllers
{
    [Authorize(Roles = "Hasta")]
    [SelectedTabAttribute("HesapBilgisi")]
    public class HesapBilgisiController : Controller
    {
        // GET: Hasta/HesapBilgisi
        public ActionResult Index()
        {
            return View("Form",new ProfileForm {
                modifyPassword = false,
                KimlikNo = Auth.User.KimlikNo,
                Username = Auth.User.Username,
                Email = Auth.User.Email,
                DogumTarihi = Auth.User.DogumTarihi,
                Telefon = Auth.User.Telefon,
                Cinsiyet = Auth.User.Cinsiyet()
            });
        }

        public ActionResult ResetPassword()
        {
            return View("Form", new ProfileForm
            {
                modifyPassword = true
            });
        }

        [HttpPost]
        public ActionResult Form(ProfileForm form)
        {
            var user = Database.Session.Load<User>(Auth.User.Id);

            if (user == null)
            {
                return HttpNotFound();
            }

            if (form.modifyPassword) //We preferred this to using Required then, adding and hiding extra fields in our view
            {
                if (form.Password == null)
                    ModelState.AddModelError("Password", "Sifre bos olamaz");
            }
            else
            {
                if (form.Email == null)
                    ModelState.AddModelError("Email", "Email gereklidir");
                if (form.Telefon == null)
                    ModelState.AddModelError("Telefon", "Telefon numara gereklidir");
            }

            if (!ModelState.IsValid)
                return View(form);

            if (form.modifyPassword)
            {
                user.SetPassword(form.Password);          
            }
            else
            {
                user.Telefon = form.Telefon;
                user.Email = form.Email;
            }

            Database.Session.Update(user);

            return RedirectToAction("index", "Home");
        }
    }
}