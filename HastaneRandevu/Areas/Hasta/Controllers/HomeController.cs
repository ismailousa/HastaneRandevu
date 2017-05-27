using HastaneRandevu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Areas.Hasta.Controllers
{
    [Authorize(Roles = "Hasta, Admin, Hasta")]
    [SelectedTabAttribute("Home")]
    public class HomeController : Controller
    {
        // GET: Hasta/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToRoute("Logout");
        }
    }
}