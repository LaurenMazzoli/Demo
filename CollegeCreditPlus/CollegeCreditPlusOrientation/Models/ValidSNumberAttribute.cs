// <copyright file="ValidSNumberAttribute.cs" company="Cuyahoga Community College">
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
namespace CollegeCreditPlusOrientation.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    /// <summary>
    /// The valid s number attribute.
    /// </summary>
    public class ValidSNumberAttribute : ValidationAttribute
    {
        /// <summary>
        /// The default error message.
        /// </summary>
        private const string DefaultErrorMessage = "SNumber Is Invalid";

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidSNumberAttribute"/> class.
        /// </summary>
        public ValidSNumberAttribute()
            : base(DefaultErrorMessage)
        {
        }

        // Override IsValid   

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="validationContext">
        /// The validation context.
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string sNumber = (string)value;

            // if(String.IsNullOrWhiteSpace(sNumber))
            // {
            //    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            // }

            if (string.IsNullOrWhiteSpace(sNumber))
            {
                return null;
            }

            Match check = Regex.Match(sNumber, @"[sS]\d+");

            // Actual comparison   
            if (!check.Success || check.Length != sNumber.Length)
            {
                var message = this.FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(message);
            }

            // Default return - This means there were no validation error   
            return null;
        }
    }
}
