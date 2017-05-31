using HastaneRandevu.Areas.Admin.ViewModels;
using HastaneRandevu.Infrastructure;
using HastaneRandevu.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Areas.Doktor.Controllers
{
    [Authorize(Roles = "Doktor")]
    public class HomeController : Controller
    {
        private const int PostPerPage = 10;
        // GET: Doktor/Doktor
        public ActionResult Index(int page = 1)
        {
            //burda hata var vozulmesı gereklı
            int randevuSayisi = Database.Session.Query<Randevu>().Where(x=>x.DoktorId == Auth.User.Id).Count();
            var Randevular = Database.Session.Query<Randevu>().Where(x => x.DoktorId == Auth.User.Id);
            var Randevulars= new List<RandevuInfo>();
            foreach (var randevu in Randevular)
            {
                Randevulars.Add(new RandevuInfo(randevu));
            }

            Randevulars
                .OrderByDescending(x => x.Tarihi)
     
                .ToList();

            return View(new RandevuList() { Randevular = new PagedData<RandevuInfo>(Randevulars, randevuSayisi, page, PostPerPage) });
            
        }
        public ActionResult RandevuIptal(int id)
        {
            var Randevu = Database.Session.Load<Randevu>(id);
            if (Randevu == null)
                return HttpNotFound();
            Randevu.Durum = "Doktor İptal";
            Database.Session.Update(Randevu);
            Database.Session.Flush();
            return RedirectToAction("index");
        }

        public ActionResult RandevuRecover(int id)
        {
            var Randevu = Database.Session.Load<Randevu>(id);
            if (Randevu == null)
                return HttpNotFound();
            Randevu.Durum = "Gelecek";
            Database.Session.Update(Randevu);
            Database.Session.Flush();
            return RedirectToAction("index");
        }
    }
}