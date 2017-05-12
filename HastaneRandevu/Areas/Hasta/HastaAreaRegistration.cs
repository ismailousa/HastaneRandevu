using System.Web.Mvc;

namespace HastaneRandevu.Areas.Hasta
{
    public class HastaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Hasta";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Hasta_default",
                "Hasta/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "HastaneRandevu.Areas.Hasta.Controllers" }
            );
        }
    }
}