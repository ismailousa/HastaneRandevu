using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Areas.Hasta.Controllers
{
    [Authorize(Roles = "Hasta")]
    public class RandevuGecmisiController : Controller
    {
        // GET: Hasta/RandevuGecmisi
        public ActionResult Index()
        {
            return View();
        }
    }
}