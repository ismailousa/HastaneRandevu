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
            int randevuSayisi = Database.Session.Query<Randevu>().Count();
            var Randevular = Database.Session.Query<Randevu>();
            var Randevulars= new List<RandevuInfo>();
            foreach (var randevu in Randevular)
            {
                Randevulars.Add(new RandevuInfo(randevu));
            }

            Randevulars
                .OrderBy(x => x.Id)
     
                .ToList();

            return View(new RandevuList() { Randevular = new PagedData<RandevuInfo>(Randevulars, randevuSayisi, page, PostPerPage) });
            
        }
        public ActionResult RandevuSil(int id)
        {
            var Randevu = Database.Session.Load<Randevu>(id);
            if (Randevu == null)
                return HttpNotFound();

            Database.Session.Delete(Randevu);
            Database.Session.Flush();
            return RedirectToAction("index");
        }
        public ActionResult HastayaYonlendir(int id)
        {
            var user = Database.Session.Load<User>(id);
            if (user == null)
                return HttpNotFound();

            //hasta areye yonlşendırcek burda hata var
           
            return RedirectToAction("index","Home","hasta");
        }
    }
}