using HastaneRandevu.Areas.Admin.ViewModels;
using HastaneRandevu.Infrastructure;
using HastaneRandevu.Models;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Areas.Hasta.Controllers
{
    [Authorize(Roles = "Hasta, Admin, Doktor")]
    [SelectedTabAttribute("Randevularim")]
    public class RandevuGecmisiController : Controller
    {
        private const int PostPerPage = 10;
        public ActionResult Index(int page = 1)
        {
            int randevuSayisi = Database.Session.Query<Randevu>().Where(x => x.HastaId == Auth.User.Id).Count();
            var Randevular = Database.Session.Query<Randevu>().Where(x => x.HastaId == Auth.User.Id);
            var Randevulars = new List<RandevuInfo>();
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
            Randevu.Durum = "İptal";
            Database.Session.Update(Randevu);
            Database.Session.Flush();
            return RedirectToAction("index");
        }


        public ActionResult PuanVer(int id, float puan)
        {
            var Randevu = Database.Session.Load<Randevu>(id);
            if (Randevu == null)
                return HttpNotFound();
            Randevu.Puan = puan;
            Database.Session.Update(Randevu);
            Database.Session.Flush();
            return RedirectToAction("index");
        }
    }
}