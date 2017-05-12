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
            var Hnamespaces = new String[] { typeof(HomeController).Namespace };

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute("Home", "", new { controller = "Home", action = "Index" }, namespaces);
            routes.MapRoute("Login", "login", new { controller = "Auth", action = "Login" }, namespaces);
            routes.MapRoute("Logout", "logout", new { controller = "Auth", action = "Logout" }, namespaces);
            routes.MapRoute("YeniUye", "newuser", new { controller = "Users", action = "New" });

            routes.MapRoute("Default_culture",
            "{culture}/{controller}/{action}/{id}",
            new { controller = "{controller}", action = "{action}", id = UrlParameter.Optional }, Hnamespaces );
            
        }
    }
}
