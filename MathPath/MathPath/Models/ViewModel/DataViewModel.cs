// <copyright file="DataViewModel.cs" company="Cuyahoga Community College">
// Copyright (c) 2020 Cuyahoga Community College.  All rights reserved.
// </copyright>
// <summary>
//  The data view model holds data needed by the index view
// </summary>
// <remarks>
// 
// </remarks>
// <source_repository>
// Current Revision : $Revision: 2 $
// Last Check In    : $Date: 2020-09-04 21:29:37-04:00 $
// Last Modification: $Modtime: 2020-09-03 16:20:50-04:00 $
// Last Change By   : $Author: lmazzol $
// $Log: /MathPath/MathPath/Models/ViewModel/DataViewModel.cs $
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
    using System.Web.Mvc;
    using MathPath.Models.ViewModel;

    /// <summary>
    /// The data view model.
    /// </summary>
    public class DataViewModel
    {
        /// <summary>
        /// The placement test list.
        /// </summary>
        public readonly List<SelectListItem> PlacementTestList = new List<SelectListItem>
            {
                new SelectListItem { Value = Controllers.HomeController.PlacementTests.AccuplacerNextGen.ToString(), Text = "ACCUPLACER (Next Gen)" },
                new SelectListItem { Value = Controllers.HomeController.PlacementTests.Aleks.ToString(), Text = "ALEKS" },
                new SelectListItem { Value = Controllers.HomeController.PlacementTests.ACT.ToString(), Text = "ACT" },
                new SelectListItem { Value = Controllers.HomeController.PlacementTests.SAT.ToString(), Text = "SAT" }
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="DataViewModel"/> class.
        /// </summary>
        public DataViewModel()
        {
            // default constructor
        }

        /// <summary>
        /// Gets or sets the major list.
        /// </summary>
        public List<SelectListItem> MajorList { get; set; }

        /// <summary>
        /// Gets or sets the placement score list.
        /// </summary>
        public List<SelectListItem> PlacementScoreList { get; set; }

        /// <summary>
        /// Gets or sets the selected major.
        /// </summary>
        [Required(ErrorMessage = "Please select a Major")]
        [DisplayName("Intended Major")]
        public string SelectedMajor { get; set; }

        /// <summary>
        /// Gets or sets the placement test.
        /// </summary>
        [DisplayName("Math Placement Test")]
        [Required(ErrorMessage = "Please select a Test")]
        public string SelectedPlacementTest { get; set; }

        /// <summary>
        /// Gets or sets the placement score.
        /// </summary>
        [DisplayName("Math Placement Test Score")]
        [Required(ErrorMessage = "Please select a Score")]
        public string SelectedPlacementScore { get; set; }
    }
}