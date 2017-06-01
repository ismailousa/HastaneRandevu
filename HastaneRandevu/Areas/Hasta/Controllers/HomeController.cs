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
using HastaneRandevu.Areas.Admin.ViewModels;

namespace HastaneRandevu.Areas.Hasta.Controllers
{
    [Authorize(Roles = "Hasta, Doktor, Admin")]
    [SelectedTabAttribute("Home")]
    public class HomeController : Controller
    {

        // GET: Hasta/Home
        public ActionResult Index()
        {
            var iladlari = Database.Session.Query<Il>().ToList();
            var ilceadlari = Database.Session.Query<Ilce>().ToList();
            var hastanadlari = Database.Session.Query<Hastane>().ToList();
            var klinikadlari = Database.Session.Query<Klinik>().ToList();
            var doktordetaylari = Database.Session.Query<User>().ToList();


            return View(new MainPage()
            {
                Iller = iladlari,
                Ilceler = ilceadlari,
                Hastaneler = hastanadlari,
                Klinikler = klinikadlari,
                Doktordetay = doktordetaylari


            });

        }
        [HttpPost]
        public ActionResult Index(MainPage form, float saat)
        {
            //var il = Database.Session.Query<Il>().ToList();

            //List<SelectListItem> IlAdlari = new List<SelectListItem>();

            //il.ForEach(x =>
            //{
            //    IlAdlari.Add(new SelectListItem { Text = x.IlAdi, Value = x.Id.ToString() });
            //});
            //var doktor = form.doktor

            var randevu = new Randevu()
            {
                DoktorId = form.doktor,
                HastaId = Auth.User.Id,
                Puan = 0,
                Durum = "Geleceke",
                TarihSaat = form.RandevuTarihi.AddHours(saat)
            }
            ;
            var randevuinfo = new RandevuInfo(randevu);
            return View("RandevuGoster",randevuinfo);

        }


    }
}