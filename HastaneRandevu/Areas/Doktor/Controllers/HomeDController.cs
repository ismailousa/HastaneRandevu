﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Areas.Doktor.Controllers
{
    public class HomeDController : Controller
    {
        // GET: Doktor/Doktor
        public ActionResult Index()
        {
            return View();
        }
    }
}