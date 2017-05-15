using HastaneRandevu.Areas.Hasta.Controllers;
using HastaneRandevu.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HastaneRandevu
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var namespaces = new String[] { typeof(AuthController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            //routes.MapRoute("Home", "Hasta", new { controller = "Home", action = "Index" });
            routes.MapRoute("Login", "", new { controller = "Auth", action = "Login" }, namespaces);
            routes.MapRoute("Logout", "logout", new { controller = "Auth", action = "Logout" }, namespaces);
            routes.MapRoute("YeniUye", "Users/New", new { controller = "Users", action = "New" });
            
        }
    }
}
