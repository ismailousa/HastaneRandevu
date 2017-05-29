using HastaneRandevu.Areas.Admin.ViewModels;
using HastaneRandevu.Infrastructure;
using HastaneRandevu.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [SelectedTabAttribute("Home")]
    public class HomeController : Controller
    {
        // GET: Admin/Admin
        private const int PostPerPage = 10;
        public ActionResult Index(int page = 1)
        {
            int kullaniciSayisi = Database.Session.Query<User>().Count();
            var kullanicilar = Database.Session.Query<User>();
            var users = new List<UserInfo>();
            foreach (var user in kullanicilar)
            {
                users.Add(new UserInfo(user));
            }

            users
                .OrderBy(x => x.AdSoyad)
                .Skip((page - 1) * PostPerPage)
                .Take(PostPerPage)
                .ToList();

            return View(new UsersList() { Users = new PagedData<UserInfo>(users, kullaniciSayisi, page, PostPerPage)});
        }
    }
}