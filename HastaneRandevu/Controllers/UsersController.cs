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
            SyncProperty(form.Roles, user.Roles);
            int refId = 0;
            SyncProperty(form.Cinsiyetler, ref refId);
            user.CinsiyetRefId = refId;
            Database.Session.Save(user);
            return RedirectToRoute("Login");
        }

        public JsonResult GetStates(string id)
        {
            List<SelectListItem> states = new List<SelectListItem>();
            switch (id)
            {
                case "1":
                    states.Add(new SelectListItem { Text = "Select", Value = "0" });
                    states.Add(new SelectListItem { Text = "ANDAMAN & NIKOBAR ISLANDS", Value = "1" });
                    states.Add(new SelectListItem { Text = "ANDHRA PRADESH", Value = "2" });
                    states.Add(new SelectListItem { Text = "ARUNACHAL PRADESH", Value = "3" });
                    states.Add(new SelectListItem { Text = "ASSAM", Value = "4" });
                    states.Add(new SelectListItem { Text = "BIHAR", Value = "5" });
                    states.Add(new SelectListItem { Text = "CHANDIGARH", Value = "6" });
                    states.Add(new SelectListItem { Text = "CHHATTISGARH", Value = "7" });
                    states.Add(new SelectListItem { Text = "DADRA & NAGAR HAVELI", Value = "8" });
                    states.Add(new SelectListItem { Text = "DAMAN & DIU", Value = "9" });
                    states.Add(new SelectListItem { Text = "GOA", Value = "10" });
                    states.Add(new SelectListItem { Text = "GUJARAT", Value = "11" });
                    states.Add(new SelectListItem { Text = "HARYANA", Value = "12" });
                    states.Add(new SelectListItem { Text = "HIMACHAL PRADESH", Value = "13" });
                    states.Add(new SelectListItem { Text = "JAMMU & KASHMIR", Value = "14" });
                    states.Add(new SelectListItem { Text = "JHARKHAND", Value = "15" });
                    states.Add(new SelectListItem { Text = "KARNATAKA", Value = "16" });
                    states.Add(new SelectListItem { Text = "KERALA", Value = "17" });
                    states.Add(new SelectListItem { Text = "LAKSHADWEEP", Value = "18" });
                    states.Add(new SelectListItem { Text = "MADHYA PRADESH", Value = "19" });
                    states.Add(new SelectListItem { Text = "MAHARASHTRA", Value = "20" });
                    states.Add(new SelectListItem { Text = "MANIPUR", Value = "21" });
                    states.Add(new SelectListItem { Text = "MEGHALAYA", Value = "22" });
                    states.Add(new SelectListItem { Text = "MIZORAM", Value = "23" });
                    states.Add(new SelectListItem { Text = "NAGALAND", Value = "24" });
                    states.Add(new SelectListItem { Text = "NCT OF DELHI", Value = "25" });
                    states.Add(new SelectListItem { Text = "ORISSA", Value = "26" });
                    states.Add(new SelectListItem { Text = "PUDUCHERRY", Value = "27" });
                    states.Add(new SelectListItem { Text = "PUNJAB", Value = "28" });
                    states.Add(new SelectListItem { Text = "RAJASTHAN", Value = "29" });
                    states.Add(new SelectListItem { Text = "SIKKIM", Value = "30" });
                    states.Add(new SelectListItem { Text = "TAMIL NADU", Value = "31" });
                    states.Add(new SelectListItem { Text = "TRIPURA", Value = "32" });
                    states.Add(new SelectListItem { Text = "UTTAR PRADESH", Value = "33" });
                    states.Add(new SelectListItem { Text = "UTTARAKHAND", Value = "34" });
                    states.Add(new SelectListItem { Text = "WEST BENGAL", Value = "35" });
                    break;
                case "2":
                    states.Add(new SelectListItem { Text = "A", Value = "36" });
                    states.Add(new SelectListItem { Text = "B", Value = "37" });
                    states.Add(new SelectListItem { Text = "C", Value = "38" });
                    break;
                case "3":
                    states.Add(new SelectListItem { Text = "1", Value = "39" });
                    states.Add(new SelectListItem { Text = "2", Value = "40" });
                    states.Add(new SelectListItem { Text = "3", Value = "41" });
                    break;
            }
            return Json(new SelectList(states, "Value", "Text"));
        }

        //public ActionResult Hastane()
        //{
        //    return View(new HastaneNew
        //    {
        //        Iller = x
        //    });
        //}
    }
}