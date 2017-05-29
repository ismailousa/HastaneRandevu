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

        public ActionResult Klinikler(int page=1)
        {
            int klinikSayisi = Database.Session.Query<Klinik>().Count();
            var klinikler = Database.Session.Query<Klinik>();
            var kliniklist = new List<KlinikInfo>();
            foreach (var klinik in klinikler)
            {
                kliniklist.Add(new KlinikInfo(klinik));
            }

            kliniklist
                .OrderBy(x =>x.KlinikAdi)
                .Skip((page - 1) * PostPerPage)
                .Take(PostPerPage)
                .ToList();

            return View(new KlinikList() { Klinikler = new PagedData<KlinikInfo>(kliniklist, klinikSayisi, page, PostPerPage) });
        }

        public ActionResult Randevular(int page = 1)
        {
            int randevuSayisi = Database.Session.Query<Randevu>().Count();
            var randevular = Database.Session.Query<Randevu>();
            var randevulist = new List<RandevuInfo>();
            foreach (var randevu in randevular)
            {
                randevulist.Add(new RandevuInfo(randevu));
            }

            randevulist
                .OrderBy(x => x.Tarihi)
                .Skip((page - 1) * PostPerPage)
                .Take(PostPerPage)
                .ToList();

            return View(new RandevuList() { Randevular = new PagedData<RandevuInfo>(randevulist, randevuSayisi, page, PostPerPage) });
        }

        public ActionResult UserSil(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            Database.Session.Delete(user);
            Database.Session.Flush();
            return RedirectToAction("index");
        }
    }
}