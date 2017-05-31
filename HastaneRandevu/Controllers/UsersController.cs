using HastaneRandevu.Areas.Admin.ViewModels;
using HastaneRandevu.Infrastructure;
using HastaneRandevu.Models;
using HastaneRandevu.ViewModels;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Controllers
{
    [SelectedTabAttribute("Register")]
    public class UsersController : Controller
    {

        private void SyncProperty(IList<RoleCheckBox> checkBoxes, IList<Role> roles)
        {
            var selectedRoles = new List<Role>();


            foreach (var role in Database.Session.Query<Role>())
            {
                var checkBox = checkBoxes.Single(c => c.Id == role.Id);
                checkBox.Name = role.Name;

                if (checkBox.IsChecked)
                    selectedRoles.Add(role);
            }


            foreach (var toAdd in selectedRoles.Where(p => !roles.Contains(p)))
            {
                roles.Add(toAdd);
            }

            foreach (var toRemove in roles.Where(p => !selectedRoles.Contains(p)).ToList())
            {
                roles.Remove(toRemove);
            }


        }

        private void SyncProperty(IList<CinsiyetRadioBox> radioButtons, ref int refId)
        {
            foreach (var cinsiyet in Database.Session.Query<Cinsiyet>())
            {
                var radioButton = radioButtons.Single(c => c.Id == cinsiyet.Id);
                radioButton.Name = cinsiyet.Name;

                if (radioButton.IsChecked)
                {
                    refId = cinsiyet.Id;
                }
            }
        }

        public ActionResult New()
        {

            return View(new UsersNew()
            {
                Cinsiyetler = Database.Session.Query<Cinsiyet>().Select(
                    cinsiyet => new CinsiyetRadioBox()
                    {
                        Id = cinsiyet.Id,
                        Name = cinsiyet.Name
                    }).ToList(),
                Roles = Database.Session.Query<Role>().Select(
                    role => new RoleCheckBox()
                    {
                        Id = role.Id,
                        Name = role.Name
                    }).ToList()
            }
            );
        }

        [HttpPost]
        public ActionResult New(UsersNew form)
        {
            if (Database.Session.Query<User>().Any(u => u.KimlikNo == form.KimlikNo))
                ModelState.AddModelError("Kimlik No", "Kimlik No tek olmasi gerekir");
            if (!ModelState.IsValid)
            {
                form.Cinsiyetler = Database.Session.Query<Cinsiyet>().Select(
                    cinsiyet => new CinsiyetRadioBox()
                    {
                        Id = cinsiyet.Id,
                        Name = cinsiyet.Name
                    }).ToList();
                form.Roles = Database.Session.Query<Role>().Select(
                    role => new RoleCheckBox()
                    {
                        Id = role.Id,
                        Name = role.Name
                    }).ToList();

                return View(form);
            }
            var user = new User
            {
                KimlikNo = form.KimlikNo,
                Username = form.Username,
                DogumTarihi = form.DogumTarihi,
                Email = form.Email,
                Telefon = form.Telefon
            };

            user.SetPassword(form.Password);
            user.Roles.Add(Database.Session.Load<Role>(3));
            int refId = 0;
            SyncProperty(form.Cinsiyetler, ref refId);
            user.CinsiyetRefId = refId;
            Database.Session.Save(user);
            Database.Session.Flush();
            return RedirectToRoute("Login");
        }

        [HttpPost]
        public ActionResult GetIlce(string Id)
        {
            int IlId;
            List<SelectListItem> IlceAdlari = new List<SelectListItem>();
            if (!string.IsNullOrEmpty(Id))
            {
                IlId = Convert.ToInt32(Id);
                var ilce = Database.Session.Query<Ilce>().Where(x => x.IlID == IlId).ToList();
                ilce.ForEach(x =>
                {
                    IlceAdlari.Add(new SelectListItem { Text = x.IlceAdi, Value = x.Id.ToString() });
                });
            }
            return Json(IlceAdlari, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Hastane()
        {
            var il = Database.Session.Query<Il>().ToList();

            List<SelectListItem> IlAdlari = new List<SelectListItem>();

            il.ForEach(x =>
            {
                IlAdlari.Add(new SelectListItem { Text = x.IlAdi, Value = x.Id.ToString() });
            });

            return View(new HastaneNew
            {
                Cinsiyetler = Database.Session.Query<Cinsiyet>().Select(
                    cinsiyet => new CinsiyetRadioBox()
                    {
                        Id = cinsiyet.Id,
                        Name = cinsiyet.Name
                    }).ToList(),
                Iller = IlAdlari
               // Ilceler = Database.Session.Query<Ilce>().ToList()
            });
        }
    }
}