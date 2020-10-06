// <copyright file="Global.asax.cs" company="Cuyahoga Community College">
// Copyright (c) 2020 Cuyahoga Community College.  All rights reserved.
// </copyright>
// <summary>
//  The Global start up class.
// </summary>
// <remarks>
// 
// </remarks>
// <source_repository>
// Current Revision : $Revision: 2 $
// Last Check In    : $Date: 2020-09-04 21:29:37-04:00 $
// Last Modification: $Modtime: 2020-08-03 21:30:53-04:00 $
// Last Change By   : $Author: lmazzol $
// $Log: /MathPath/MathPath/Global.asax.cs $
// 
// Revision: 2   Date: 2020-09-05 01:29:37Z   User: lmazzol 
// Completed development 
// 
// Revision: 1   Date: 2020-08-04 01:16:18Z   User: lmazzol 
// </source_repository>
namespace MathPath
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using MathPath.Data;

    /// <summary>
/// The application.
/// </summary>
public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// The application_ start method.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AppDbConnection.InitializeConnections();
        }
    }
}
