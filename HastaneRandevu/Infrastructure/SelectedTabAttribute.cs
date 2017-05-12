﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HastaneRandevu.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

    public class SelectedTabAttribute : ActionFilterAttribute
    {
        private readonly string _SelectedTab;
        public SelectedTabAttribute(string SelectedTab)
        {
            _SelectedTab = SelectedTab;
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.SelectedTab = _SelectedTab;
        }

    }


}