using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Areas.Hasta.Controllers
{
    [Authorize(Roles = "hasta")]
    public class HomeController : Controller
    {
        // GET: Hasta/Home
        public ActionResult Index()
        {
            return Content("Hosgeldin Hasta");
        }
    }
}