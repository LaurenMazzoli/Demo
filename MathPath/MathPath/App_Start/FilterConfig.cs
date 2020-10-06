// <copyright file="FilterConfig.cs" company="Cuyahoga Community College">
// Copyright (c) 2020 Cuyahoga Community College.  All rights reserved.
// </copyright>
// <summary>
// Filter Config 
// </summary>
// <remarks>
// 
// </remarks>
// <source_repository>
// Current Revision : $Revision: 2 $
// Last Check In    : $Date: 2020-09-04 21:29:37-04:00 $
// Last Modification: $Modtime: 2020-08-03 21:27:24-04:00 $
// Last Change By   : $Author: lmazzol $
// $Log: /MathPath/MathPath/App_Start/FilterConfig.cs $
// 
// Revision: 2   Date: 2020-09-05 01:29:37Z   User: lmazzol 
// Completed development 
// </source_repository>
namespace MathPath
{
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// The filter config class.
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// The register global filters.
        /// </summary>
        /// <param name="filters">
        /// The filters.
        /// </param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
