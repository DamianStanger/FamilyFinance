using System.Web.Mvc;
using System.Web.Routing;

namespace FamilyFinance.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Statement",
                url: "Accounts/{accountId}/statements/{year}/{month}",
                defaults: new {controller = "Statements", action = "Statement"}
            );

            routes.MapRoute(
                name: "Statements",
                url: "Accounts/{accountId}/statements",
                defaults: new { controller = "Statements", action = "Statements" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}