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
        // GET: Users
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
    }
}