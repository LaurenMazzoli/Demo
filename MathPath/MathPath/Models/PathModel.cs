// <copyright file="PathModel.cs" company="Cuyahoga Community College">
// Copyright (c) 2020 Cuyahoga Community College.  All rights reserved.
// </copyright>
// <summary>
// The PathModel Class holds data for a given path.
// </summary>
// <remarks>
// 
// </remarks>
// <source_repository>
// Current Revision : $Revision: 2 $
// Last Check In    : $Date: 2020-09-04 21:29:37-04:00 $
// Last Modification: $Modtime: 2020-08-05 09:11:20-04:00 $
// Last Change By   : $Author: lmazzol $
// $Log: /MathPath/MathPath/Models/PathModel.cs $
// 
// Revision: 2   Date: 2020-09-05 01:29:37Z   User: lmazzol 
// Completed development 
// 
// Revision: 1   Date: 2020-08-04 01:16:18Z   User: lmazzol 
// </source_repository>

namespace MathPath.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection.Emit;
    using System.Web;
    using System.Web.UI.WebControls;

    using DAL;

    /// <summary>
    /// The path model class holds the data for a path.
    /// </summary>
    public class PathModel
    {
        /// <summary>
        /// Gets or sets the program path name.
        /// </summary>
        public string ProgramPathName { get; set; }

        /// <summary>
        /// Gets or sets the path name.
        /// </summary>
        public string PathName { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the required course.
        /// </summary>
        public string RequiredCourse { get; set; }

        /// <summary>
        /// Gets or sets the course list.
        /// </summary>
        public List<Course> CourseList { get; set; }

        /// <summary>
        /// Gets or sets the placement course.
        /// </summary>
        public int PlacementCourseId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether tested out.
        /// </summary>
        public bool TestedOut { get; set;  }
    }
}