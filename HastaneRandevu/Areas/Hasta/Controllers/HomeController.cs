using HastaneRandevu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HastaneRandevu.Models;
using HastaneRandevu.Areas.Hasta.ViewModels;
using NHibernate.Linq;
using Microsoft.Ajax.Utilities;
using NHibernate.Util;
using HastaneRandevu;

namespace HastaneRandevu.Areas.Hasta.Controllers
{
    [Authorize(Roles = "Hasta, Doktor, Admin")]
    [SelectedTabAttribute("Home")]
    public class HomeController : Controller
    {

        // GET: Hasta/Home
        public ActionResult Index()
        {
            var il = Database.Session.Query<Il>().ToList();

            List<SelectListItem> IlAdlari = new List<SelectListItem>();

            il.ForEach(x =>
            {
                IlAdlari.Add(new SelectListItem { Text = x.IlAdi, Value = x.Id.ToString() });
            });

            return View(new MainPage
            {
                Iller = IlAdlari

            });

        }
        [HttpPost]
        public ActionResult Index(MainPage form)
        {
            var il = Database.Session.Query<Il>().ToList();

            List<SelectListItem> IlAdlari = new List<SelectListItem>();

            il.ForEach(x =>
            {
                IlAdlari.Add(new SelectListItem { Text = x.IlAdi, Value = x.Id.ToString() });
            });

            return View();//okay

        }


        //[HttpPost]
        //public ActionResult GetBilgiler(string Id)
        //{
        //    int IlId, IlceId, Klinikid;

        //    List<SelectListItem> IlceAdlari = new List<SelectListItem>();
        //    if (!string.IsNullOrEmpty(Id))
        //    {
        //        IlId = Convert.ToInt32(Id);
        //        var ilce = Database.Session.Query<Ilce>().Where(x => x.IlID == IlId).ToList();
        //        ilce.ForEach(x =>
        //        {
        //            IlceAdlari.Add(new SelectListItem { Text = x.IlceAdi, Value = x.Id.ToString() });
        //        });
        //    }


        //    List<SelectListItem> HastaneAdlari = new List<SelectListItem>();
        //    if (!string.IsNullOrEmpty(Id))
        //    {
        //        IlceId = Convert.ToInt32(Id);
        //        var hastane = Database.Session.Query<Hastane>().Where(x => x.IlceID == IlceId).ToList();
        //        hastane.ForEach(x =>
        //    {
        //        HastaneAdlari.Add(new SelectListItem { Text = x.HastaneAdi, Value = x.Id.ToString() });

        //    });


        //        var klinik = Database.Session.Query<Klinik>().ToList();
        //        List<SelectListItem> klinikAdlari = new List<SelectListItem>();
        //        klinik.ForEach(x =>
        //        {
        //            klinikAdlari.Add(new SelectListItem { Text = x.KlinikAdi, Value = x.Id.ToString() });
        //        });




        //        List<SelectListItem> doktorAdlari = new List<SelectListItem>();
        //        if (!string.IsNullOrEmpty(Id))
        //        {
        //            Klinikid = Convert.ToInt32(Id);
        //            var doktor = Database.Session.Query<Doctor>().Where(x => x.KlinikId == Klinikid).ToList();
        //            doktor.ForEach(x =>
        //            {
        //                doktorAdlari.Add(new SelectListItem { Text = x.DoktorAdi, Value = x.DoktorId.ToString() });

        //            });
        //            return View(new MainPage() {
        //                Ilceler = IlceAdlari,
        //                hastaneler=HastaneAdlari,
        //                Klinikler = klinikAdlari,
        //                Doktorlar =doktorAdlari,

        //            });
        //            //return Json(IlceAdlari,HastaneAdlari,klinikAdlari,doktorAdlari, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //}


    }
}


//public ActionResult Logout()
//{
//    return RedirectToRoute("Logout");
//}
