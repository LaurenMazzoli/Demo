// <copyright file="Major.cs" company="Cuyahoga Community College">
// Copyright (c) 2020 Cuyahoga Community College.  All rights reserved.
// </copyright>
// <summary>
//  The Major  Class is used to store all the data needed for a major from the program table and its related degree or certificate table.
// </summary>
// <remarks>
// 
// </remarks>
// <source_repository>
// Current Revision : $Revision: 1 $
// Last Check In    : $Date: 2020-08-03 21:16:18-04:00 $
// Last Modification: $Modtime: 2020-08-03 21:16:16-04:00 $
// Last Change By   : $Author: lmazzol $
// $Log: /MathPath/MathPath/Models/Major.cs $
// 
// Revision: 1   Date: 2020-08-04 01:16:18Z   User: lmazzol 
// </source_repository>
namespace MathPath.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// The major object stores all the data needed from the program, and its associated degree or certificate table.
    /// </summary>
    public class Major
    {
        /// <summary>
        /// Gets or sets the program id.
        /// </summary>
        public int ProgramId { get; set; }

        /// <summary>
        /// Gets or sets the major name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the major program code.
        /// </summary>
        public string ProgramCode { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// Gets or sets the type of Degree or Certificate.
        /// </summary>
        public string Type { get; set; }
    }
}