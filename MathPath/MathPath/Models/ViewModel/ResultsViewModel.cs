// <copyright file="ResultsViewModel.cs" company="Cuyahoga Community College">
// Copyright (c) 2020 Cuyahoga Community College.  All rights reserved.
// </copyright>
// <summary>
//  This will hold the math paths data for the selected program
// </summary>
// <remarks>
// 
// </remarks>
// <source_repository>
// Current Revision : $Revision: 2 $
// Last Check In    : $Date: 2020-09-04 21:29:37-04:00 $
// Last Modification: $Modtime: 2020-09-04 19:24:14-04:00 $
// Last Change By   : $Author: lmazzol $
// $Log: /MathPath/MathPath/Models/ViewModel/ResultsViewModel.cs $
// 
// Revision: 2   Date: 2020-09-05 01:29:37Z   User: lmazzol 
// Completed development 
// 
// Revision: 1   Date: 2020-08-04 01:16:18Z   User: lmazzol 
// </source_repository>

namespace MathPath.Models.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    using DAL;

    /// <summary>
    /// The results view model.
    /// </summary>
    public class ResultsViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsViewModel"/> class.
        /// </summary>
        public ResultsViewModel()
        {
            // default constructor
        }

        ///// <summary>
        ///// Gets or sets the results.
        ///// </summary>
        /////public string Results { get; set; }

        /// <summary>
        /// Gets or sets the major entered by the user.
        /// </summary>
        [DisplayName("Major")]
        public string Major { get; set; }

        /// <summary>
        /// Gets or sets the max number courses for all paths of this program.
        /// </summary>
        public int MaxNumCourses { get; set; }

        /// <summary>
        /// Gets or sets the placement test information entered by the user.
        /// </summary>
        [DisplayName("Placement")]
        public string Placement { get; set; }

        /// <summary>
        /// Gets or sets the list of paths.
        /// </summary> 
        public List<PathModel> Paths { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }
    }
}