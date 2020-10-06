// <copyright file="BundleConfig.cs" company="Cuyahoga Community College">
// Copyright (c) 2020 Cuyahoga Community College.  All rights reserved.
// </copyright>
// <summary>
// The Bundle Config class
// </summary>
// <remarks>
// 
// </remarks>
// <source_repository>
// Current Revision : $Revision: 2 $
// Last Check In    : $Date: 2020-09-04 21:29:37-04:00 $
// Last Modification: $Modtime: 2020-08-03 21:28:05-04:00 $
// Last Change By   : $Author: lmazzol $
// $Log: /MathPath/MathPath/App_Start/BundleConfig.cs $
// 
// Revision: 2   Date: 2020-09-05 01:29:37Z   User: lmazzol 
// Completed development 
// </source_repository>
namespace MathPath
{
    using System.Web;
    using System.Web.Optimization;

    /// <summary>
    /// The bundle config class.
    /// </summary>
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862

        /// <summary>
        /// The register bundles method.
        /// </summary>
        /// <param name="bundles">
        /// The bundles.
        /// </param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
