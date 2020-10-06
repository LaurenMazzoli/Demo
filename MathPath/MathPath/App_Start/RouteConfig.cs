// <copyright file="RouteConfig.cs" company="Cuyahoga Community College">
// Copyright (c) 2020 Cuyahoga Community College.  All rights reserved.
// </copyright>
// <summary>
// 
// </summary>
// <remarks>
// 
// </remarks>
// <source_repository>
// Current Revision : $Revision: 2 $
// Last Check In    : $Date: 2020-09-04 21:29:37-04:00 $
// Last Modification: $Modtime: 2020-08-03 21:33:52-04:00 $
// Last Change By   : $Author: lmazzol $
// $Log: /MathPath/MathPath/App_Start/RouteConfig.cs $
// 
// Revision: 2   Date: 2020-09-05 01:29:37Z   User: lmazzol 
// Completed development 
// </source_repository>

namespace MathPath
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// The route config.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// The register routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
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
