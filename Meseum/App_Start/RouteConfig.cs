using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Meseum
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
             name: "Inventory",
             url: "AllInventory",
             defaults: new { controller = "Inventories", action = "Index", id = UrlParameter.Optional }
           );


            routes.MapRoute(
             name: "UploadImage",
             url: "upload",
             defaults: new { controller = "Inventories", action = "Upload", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "Dashboard",
             url: "home/dashboard",
             defaults: new { controller = "Home", action = "IndexAdmin", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
