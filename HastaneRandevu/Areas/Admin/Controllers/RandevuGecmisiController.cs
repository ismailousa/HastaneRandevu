using HastaneRandevu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [SelectedTabAttribute("Randevularim")]
    public class RandevuGecmisiController : Controller
    {
        // GET: Admin/RandevuGecmisi
        public ActionResult Index()
        {
            return View();
        }
    }
}