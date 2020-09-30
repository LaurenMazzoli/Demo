namespace CollegeCreditPlusOrientation
{
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Registers route patterns.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Registers the routes in the RouteCollection.
        /// </summary>
        /// <param name="routes">routes instance</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}
