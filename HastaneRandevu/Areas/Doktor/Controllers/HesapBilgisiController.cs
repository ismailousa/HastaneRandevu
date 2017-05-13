using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Areas.Doktor.Controllers
{
    [Authorize(Roles = "Doktor")]
    public class HesapBilgisiController : Controller
    {
        // GET: Doktor/HesapBilgisi
        public ActionResult Index()
        {
            return View();
        }
    }
}