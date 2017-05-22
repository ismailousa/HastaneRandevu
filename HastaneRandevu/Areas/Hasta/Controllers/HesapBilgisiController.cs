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
            var cinsiyet = Database.Session.Load<Cinsiyet>(Auth.User.CinsiyetRefId);

            return View("Form",new ProfileForm {
                modifyPassword = false,
                KimlikNo = Auth.User.KimlikNo,
                Username = Auth.User.Username,
                Email = Auth.User.Email,
                DogumTarihi = Auth.User.DogumTarihi,
                Telefon = Auth.User.Telefon,
                Cinsiyet = cinsiyet.Name
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

            if (form.modifyPassword)
            {
                //if (!ModelState.IsValid)
                //    return View(form);

                user.SetPassword(form.Password);


                Database.Session.Update(user);

                return RedirectToAction("index","Home");
            }

            return Content("Bilgi Guncelleme daha yazilmadi");
        }
    }
}