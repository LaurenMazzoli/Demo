// <copyright file="Form.cs" company="Cuyahoga Community College">
// Copyright (c) 2019 Cuyahoga Community College.  All rights reserved.
// </copyright>
// <summary>
// This is the college credit plus orientation input form view model. 
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
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.ExceptionServices;
    using System.Web.Mvc;

    using EventRegistration.Interface.Models;

    using FormHelper.Validators;

    /// <summary>
    /// The form.
    /// </summary>
    public class Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Form"/> class.
        /// </summary>
        public Form()
        {
            this.Events = new List<SelectListItem>();
            this.GradYear = System.DateTime.Now.Year;
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the selected event.
        /// </summary>
        [Required(ErrorMessage ="Event is required")]
        [DisplayName("Event")] public string SelectedEvent { get; set; }

        // public string AstraEventId { get; set; }

        /// <summary>
        /// Gets or sets the program event id.
        /// </summary>
        public int ProgramEventId { get; set; }

        // todo: Create the eventdata class in the event registration web service library. This is the format that the json payload will be returned in.) 

        /// <summary>
        /// Gets or sets the events.
        /// </summary>
        public IEnumerable<SelectListItem> Events { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required(ErrorMessage = "First Name is required"), DisplayName("First Name"),
         MaxLength(50, ErrorMessage = "First Name must be less than or equal to 50 characters.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required(ErrorMessage = "Last Name is required"), DisplayName("Last Name"),
         MaxLength(50, ErrorMessage = "Last Name must be less than or equal to 50 characters.")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [Required(ErrorMessage = "Address is required"), DisplayName("Address"),
         MaxLength(100, ErrorMessage = "Address must be less than or equal to 100 characters.")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [Required(ErrorMessage = "City is required"), DisplayName("City"),
         MaxLength(100, ErrorMessage = "City must be less than or equal to 100 characters.")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [Required(ErrorMessage = "State is required"), DisplayName("State")] public string State { get; set; }

        /// <summary>
        /// Gets or sets the states.
        /// </summary>
        public IEnumerable<SelectListItem> States { get; set; }

        /// <summary>
        /// Gets or sets the Zip code.
        /// </summary>
        [Required(ErrorMessage = "Zipcode is required"), DisplayName("Zipcode"), FormHelper.Validators.ValidZipCode]
        public string Zipcode { get; set; }

        // [DisplayName("Phone Number (###-###-####)"), DataType(DataType.PhoneNumber), ValidPhoneNumber]

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [Required(ErrorMessage = "Phone is required"), DisplayName("Phone Number"), DataType(DataType.PhoneNumber), ValidPhoneNumber]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [Required(ErrorMessage = "Email is required"), DisplayName("Email Address"), DataType(DataType.EmailAddress), ValidEmail]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the tri c number.
        /// </summary>
        [DisplayName("Tri-C ID Number"), ValidSNumber]
        public string TriCNumber { get; set; }

        /// <summary>
        /// Gets or sets the school name.
        /// </summary>
        [Required(ErrorMessage = "School Name is required"), DisplayName("School Name"), MaxLength(100, ErrorMessage = "School Name must be less than or equal to 100 characters.")]
        public string SchoolName { get; set; }

        /// <summary>
        /// Gets or sets the high school graduation year.
        /// Added this field for proof of concept of two custom fields stored in the registration details
        /// </summary>
        [Required(ErrorMessage = "Grad Year is required"), DisplayName("High School Graduation Year"), Range(2020,2050)]
        public int GradYear { get; set; }

        /// <summary>
        /// Gets or sets the number of people attending the event.
        /// </summary>
        [Required(ErrorMessage = "Number Attending is required"), DisplayName("Number Attending")]

        // todo: test the int dropdown with 1-4
        [Range(1, 6)]
        public int NbrAttending { get; set; }
    }
}