using EDevletSimulasyon.Areas.MesajEntegrasyon.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EDevletSimulasyon
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Root URL routes to MesajEntegrasyon area's MesajController's Index action
            routes.MapRoute(
                name: "Default",
                url: "",
                defaults: new { controller = "Mesaj", action = "Index", area = "MesajEntegrasyon" }
            );

            // General fallback route for non-area controllers
            routes.MapRoute(
                name: "General",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
