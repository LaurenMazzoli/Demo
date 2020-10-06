// <copyright file="AppDBConnection.cs" company="Cuyahoga Community College">
// Copyright (c) 2020 Cuyahoga Community College.  All rights reserved.
// </copyright>
// <summary>
// This class retreives and stores the database credentials for this application
// </summary>
// <remarks>
// 
// </remarks>
// <source_repository>
// </source_repository>
namespace MathPath.Data
{
    using System;
    using System.Data.Common;
    using DbConnection;

    using MathPath.Models;

    using NLog;

    /// <summary>
    /// Gets the connection strings needed for the application.
    /// </summary>
    public static class AppDbConnection
    {
        /// <summary>
        /// Gets the Web SQL Apps connection string.
        /// </summary>
        public static string WebSqlFormsConn { get; private set; }

        /// <summary>
        /// The initialize connections.
        /// </summary>
        public static void InitializeConnections()
        {
            string connection = Connection.GetSingleConnectionString(Properties.Settings.Default.ApplicationId);
             WebSqlFormsConn = Connection.GetEntityFrameworkConnectionString(connection);
         }
    }
}