// <copyright file="Global.asax.cs" company="Cuyahoga Community College">
// Copyright (c) 2019 Cuyahoga Community College.  All rights reserved.
// </copyright>
// <summary>
// 
// </summary>
// <remarks>
// 
// </remarks>
// <source_repository>
// Current Revision : $Revision: $
// Last Check In    : $Date: $
// Last Modification: $Modtime: $
// Last Change By   : $Author: $
// $Log: $
// </source_repository>

namespace CollegeCreditPlusOrientation
{
    using System.Net.Http;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    /// <summary>
    /// Handles events.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Loads and initializes the data.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// The session_ start event sets the alert test variable.
        /// </summary>
        protected void Session_Start()
        {
            this.Session["AlertMessage"] = Properties.Settings.Default.AlertMessage;
        }
    }
}
