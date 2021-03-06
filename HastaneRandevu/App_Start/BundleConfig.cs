﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace HastaneRandevu.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles (BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/admin/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/Admin.css")
                );
            bundles.Add(new StyleBundle("~/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/Site.css")
                .Include("~/content/styles/bootstrap-datetimepicker.css")
                .Include("~/content/styles/jquery-ui.css")
                );
            bundles.Add(new StyleBundle("~/doktor/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/Doktor.css")
                );

            bundles.Add(new ScriptBundle("~/scripts")
                   .Include("~/scripts/jquery-3.1.1.js")
                   .Include("~/scripts/jquery.validate.js")
                   .Include("~/scripts/jquery.validate.unobtrusive.js")
                   .Include("~/scripts/bootstrap.js")
                   .Include("~/scripts/moment.js")
                   .Include("~/scripts/bootstrap-datetimepicker.js")
                   .Include("~/scripts/jquery-ui-1.12.1.js")
               );


            bundles.Add(new ScriptBundle("~/admin/scripts")
                    .Include("~/scripts/jquery-3.1.1.js")
                    .Include("~/scripts/jquery.validate.js")
                    .Include("~/scripts/jquery.validate.unobtrusive.js")
                    .Include("~/scripts/bootstrap.js")
               );
        }
    }
}