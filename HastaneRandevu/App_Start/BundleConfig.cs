using System;
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
                );
            bundles.Add(new StyleBundle("~/doktor/styles")
                .Include("~/content/styles/bootstrap.css")
                .Include("~/content/styles/Doktor.css")
                );

            bundles.Add(new ScriptBundle("~/scripts")
                   .Include("~/scripts/jquery-1.8.0.js")
                   .Include("~/scripts/jquery.validate.js")
                   .Include("~/scripts/jquery.validate.unobtrusive.js")
                   .Include("~/scripts/bootstrap.js")
                   .Include("~/scripts/bootstrap-datetimepicker.js")
                   .Include("~/scripts/myDatePicker.js")
               );


            bundles.Add(new ScriptBundle("~/admin/scripts")
                    .Include("~/scripts/jquery-1.8.0.js")
                    .Include("~/scripts/jquery.validate.js")
                    .Include("~/scripts/jquery.validate.unobtrusive.js")
                    .Include("~/scripts/bootstrap.js")
               );
        }
    }
}