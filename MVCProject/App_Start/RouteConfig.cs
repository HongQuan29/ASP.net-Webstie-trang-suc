using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCProject
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Search",
                url: "search",
                defaults: new { controller = "product", action = "searchproduct", id = UrlParameter.Optional },
                namespaces: new[] { "MVCProject.Controllers" }
            );

            routes.MapRoute(
                name: "Product",
                url: "product",
                defaults: new { controller = "product", action = "index", id = UrlParameter.Optional },
                namespaces: new[] { "MVCProject.Controllers" }
            );

            routes.MapRoute(
                name: "Slug",
                url: "{slug}",
                defaults: new { controller = "site", action = "index", id = UrlParameter.Optional },
                namespaces: new[] { "MVCProject.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "site", action = "home", id = UrlParameter.Optional },
                namespaces: new[] { "MVCProject.Controllers" }
            );
        }
    }
}
