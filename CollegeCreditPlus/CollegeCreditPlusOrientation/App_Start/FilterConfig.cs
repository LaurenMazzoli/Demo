namespace CollegeCreditPlusOrientation
{
    using System.Web.Mvc;

    /// <summary>
    /// Registers global MVC filters.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Renders error page when an unhandled exception occurs.
        /// </summary>
        /// <param name="filters">filter instance</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
