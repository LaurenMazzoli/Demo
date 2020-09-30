// <copyright file="StatesRepository.cs" company="Cuyahoga Community College">
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
namespace CollegeCreditPlusOrientation.Helpers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    /// <summary>
    /// The states repository.
    /// </summary>
    public static class StatesRepository
    {
        /// <summary>
        /// This method creates select list of states
        /// </summary>
        /// <returns>
        ///  A collection of select list items containing state codes and state names t be used in a drop down box. />.
        /// </returns>
        public static IEnumerable<SelectListItem> GetStates()
        {
            List<SelectListItem> states = new List<SelectListItem>();
            states.Add(new SelectListItem { Value = "AL", Text = "Alabama" });
            states.Add(new SelectListItem { Value = "AK", Text = "Alaska" });
            states.Add(new SelectListItem { Value = "AZ", Text = "Arizona" });
            states.Add(new SelectListItem { Value = "AR", Text = "Arkansas" });
            states.Add(new SelectListItem { Value = "CA", Text = "California" });
            states.Add(new SelectListItem { Value = "CO", Text = "Colorado" });
            states.Add(new SelectListItem { Value = "CT", Text = "Connecticut" });
            states.Add(new SelectListItem { Value = "DE", Text = "Delaware" });
            states.Add(new SelectListItem { Value = "DC", Text = "District of Columbia" });
            states.Add(new SelectListItem { Value = "FL", Text = "Florida" });
            states.Add(new SelectListItem { Value = "GA", Text = "Georgia" });
            states.Add(new SelectListItem { Value = "HI", Text = "Hawaii" });
            states.Add(new SelectListItem { Value = "ID", Text = "Idaho" });
            states.Add(new SelectListItem { Value = "IL", Text = "Illinois" });
            states.Add(new SelectListItem { Value = "IN", Text = "Indiana" });
            states.Add(new SelectListItem { Value = "IA", Text = "Iowa" });
            states.Add(new SelectListItem { Value = "KS", Text = "Kansas" });
            states.Add(new SelectListItem { Value = "KY", Text = "Kentucky" });
            states.Add(new SelectListItem { Value = "LA", Text = "Louisiana" });
            states.Add(new SelectListItem { Value = "ME", Text = "Maine" });
            states.Add(new SelectListItem { Value = "MD", Text = "Maryland" });
            states.Add(new SelectListItem { Value = "MA", Text = "Massachusetts" });
            states.Add(new SelectListItem { Value = "MI", Text = "Michigan" });
            states.Add(new SelectListItem { Value = "MN", Text = "Minnesota" });
            states.Add(new SelectListItem { Value = "MS", Text = "Mississippi" });
            states.Add(new SelectListItem { Value = "MO", Text = "Missouri" });
            states.Add(new SelectListItem { Value = "MT", Text = "Montana" });
            states.Add(new SelectListItem { Value = "NE", Text = "Nebraska" });
            states.Add(new SelectListItem { Value = "NV", Text = "Nevada" });
            states.Add(new SelectListItem { Value = "NH", Text = "New Hampshire" });
            states.Add(new SelectListItem { Value = "NJ", Text = "New Jersey" });
            states.Add(new SelectListItem { Value = "NM", Text = "New Mexico" });
            states.Add(new SelectListItem { Value = "NY", Text = "New York" });
            states.Add(new SelectListItem { Value = "NC", Text = "North Carolina" });
            states.Add(new SelectListItem { Value = "ND", Text = "North Dakota" });
            states.Add(new SelectListItem { Value = "OH", Text = "Ohio" });
            states.Add(new SelectListItem { Value = "OK", Text = "Oklahoma" });
            states.Add(new SelectListItem { Value = "OR", Text = "Oregon" });
            states.Add(new SelectListItem { Value = "PA", Text = "Pennsylvania" });
            states.Add(new SelectListItem { Value = "RI", Text = "Rhode Island" });
            states.Add(new SelectListItem { Value = "SC", Text = "South Carolina" });
            states.Add(new SelectListItem { Value = "SD", Text = "South Dakota" });
            states.Add(new SelectListItem { Value = "TN", Text = "Tennessee" });
            states.Add(new SelectListItem { Value = "TX", Text = "Texas" });
            states.Add(new SelectListItem { Value = "UT", Text = "Utah" });
            states.Add(new SelectListItem { Value = "VT", Text = "Vermont" });
            states.Add(new SelectListItem { Value = "VA", Text = "Virginia" });
            states.Add(new SelectListItem { Value = "WA", Text = "Washington" });
            states.Add(new SelectListItem { Value = "WV", Text = "West Virginia" });

            return states;
        }
    }
}
