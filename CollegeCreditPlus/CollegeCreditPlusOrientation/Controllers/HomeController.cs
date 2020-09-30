// <copyright file="HomeController.cs" company="Cuyahoga Community College">
// Copyright (c) 2019 Cuyahoga Community College.  All rights reserved.
// </copyright>
// <summary>
// The home controller calls the Event Registration API to get event information, displays an input form, validates
// the user input data and calls the web api to insert a registration record. 
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

using Microsoft.Ajax.Utilities;

namespace CollegeCreditPlusOrientation.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Reflection.Emit;
    using System.ServiceModel.Security;
    using System.Web.Configuration;
    using System.Web.Mvc;
    using System.Web.UI.WebControls;

    using EventRegistration.Interface;
    using EventRegistration.Interface.Models;

    using FormHelper;

    using Helpers;

    using Models;

    using NLog;
    using Newtonsoft.Json;
    using Recaptcha.Web;
    using Recaptcha.Web.Mvc;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// This field is used to call the event registration API.
        /// </summary>
        private static readonly string EventRegistrationId = Properties.Settings.Default.EventRegistrationGUID.ToString();

        private static readonly int SetBackDays = Properties.Settings.Default.SetBackDays;

        private static DateTime? startDate = null;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The index.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            if (Properties.Settings.Default.SetBackDays != 0)
            {
                startDate = DateTime.Now.Date.AddDays(-1 * SetBackDays);
            }

            this.logger.Trace($"Index Calling Interface.GetEventsByProgram using {EventRegistrationId}");
            var events = Interface.GetEventsByProgram(EventRegistrationId, startDate);
            if (!events.Any())
            {
                return this.RedirectToAction("Closed");
            }

            // create states drop down list
            IEnumerable<SelectListItem> states = StatesRepository.GetStates();
            IEnumerable<SelectListItem> selectList = GetSelectList(events);

            Form form = new Form()
                            {
                                States = states, 
                                Events = selectList, 
                                Message = Properties.Settings.Default.AlertMessage
                            };
  
            return this.View(form);
        }

        /// <summary>
        /// The index.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Index(Form data)
        {
            this.logger.Trace($"Index Post Calling Interface.GetEventsByProgram to get drop down list of events for Program {EventRegistrationId}");
            if (Properties.Settings.Default.SetBackDays != 0)
            {
                startDate = DateTime.Now.Date.AddDays(-1 * SetBackDays);
            }

            List<EventData> events = Interface.GetEventsByProgram(EventRegistrationId, startDate);
            data.Events = GetSelectList(events);

            if (!events.Any())
            {
                return this.RedirectToAction("Closed");
            }

            // create states drop down list - note - move to default constructor
            IEnumerable<SelectListItem> states = StatesRepository.GetStates();
            data.States = states;
            if (!Properties.Settings.Default.devMode && this.ModelState.IsValid)
            {
                this.VerifyCaptcha("CaptchaError");
            }

            this.ViewBag.PublicKey = WebConfigurationManager.AppSettings["recaptchaPublicKey"];

            if (!this.ModelState.IsValid)
            {
                return this.View(data);
            }

            EventData item = Interface.GetEvent(EventRegistrationId, data.SelectedEvent);
            if (item == null)
            {
                return this.RedirectToAction("Error");
            }

            // todo: consider moving dup check to AddRegistration 
            if (!Properties.Settings.Default.AllowDuplicates)
            {
                if (this.CheckDuplicate(data))
                {
                    string ccpEmail = this.GetCampusEmail(item.CampusName);
                    return this.View("Duplicate", null, ccpEmail);
                }
            }

            RegistrationData reg = this.AddRegistration(data);

            // todo : need a status and/or message to be returned to check for ok or dup.
            if (reg == null)
            {
                // API returned an error
                return this.RedirectToAction("Error");
            }

            bool isSentEmail = this.SendEmail(reg.EmailAddress, item);

            return this.RedirectToAction(isSentEmail ? "Thanks" : "Error");
        }

        /// <summary>
        /// This method displays the generic error screen.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Error()
        {
            return this.View();
        }

        /// <summary>
        /// The thanks.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Thanks()
        {
            // todo: look at CCP CMSD program and copy code there to display the user data to the screen
            // send the email body message to the view
            var message = (string)this.HttpContext.Session["Message"];
            return this.View(model: message);
        }

        /// <summary>
        /// The close method displays a message. It is invoked when there are no Astra events to register for. 
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Closed()
        {
            string message = Properties.Settings.Default.ClosedMessage;
            return this.View(model: message);
        }

        /// <summary>
        /// This method sends a verification email to the user at the email address they entered in the form.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool SendEmail(string emailAddress, EventData item)
        {
            string emailBody = GetEmailMessage(item);
            this.HttpContext.Session["Message"] = emailBody;
            string emailTo = Properties.Settings.Default.devMode
                              ? Properties.Settings.Default.simulatedEmail
                              : emailAddress;
            EmailProcess ep = new EmailProcess(
                Properties.Settings.Default.EmailHost,
                Properties.Settings.Default.EmailPort,
                Properties.Settings.Default.EmailFrom);

          return ep.SendEmail(emailTo, Properties.Settings.Default.EmailSubject, emailBody);
        }

        /// <summary>
        /// This method sets the message in the body of the email. 
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetEmailMessage(EventData data)
        {
           // EventDataModel item = Interface.GetEvent(ConsumerAppGuid, data.ProgramEventGuid.ToString());
            string programMessage = string.IsNullOrEmpty(data.ProgramEmailMessage)
                                        ? Properties.Settings.Default.ThanksMsg1
                                        : data.ProgramEmailMessage;

            string campusEmail = "If you have questions, please contact the CCP Coordinator at your campus:<br/><br/>"
                               + $"<a href=\"mailto:{Properties.Settings.Default.EastEmail}?subject=Virtual CCP New Student Orientation Information Request\"> {Properties.Settings.Default.EastEmail}<br/>"
                               + $"<a href=\"mailto:{Properties.Settings.Default.MetroEmail}?subject=Virtual CCP New Student Orientation Information Request\"> {Properties.Settings.Default.MetroEmail}<br/>"
                               + $"<a href=\"mailto:{Properties.Settings.Default.BrunswickEmail}?subject=Virtual CCP New Student Orientation Information Request\"> {Properties.Settings.Default.BrunswickEmail}<br/>"
                               + $"<a href=\"mailto:{Properties.Settings.Default.WestshoreEmail}?subject=Virtual CCP New Student Orientation Information Request\"> {Properties.Settings.Default.WestshoreEmail}";
                               
            string message = $"<p> {programMessage} </p><br/>" + 
                             $"<h6>Event Information</h6>" +
                             $"<p>{data.GetEventInfo()}</p>" + 
                             $"<p>{data.CampusAddress}</p><br/>" +
                             $"<p>{campusEmail}</p><br/>" +
                             $"<p>{Properties.Settings.Default.ThanksMsg2}</p>";

            return message;
        }

        /// <summary>
        /// Converts a list of event data objects to a select list.
        /// </summary>
        /// <param name="events">
        /// The events.
        /// </param>
        /// <returns>
        /// A select list with the Event Meeting Id as the value and the campus name, event date,
        /// event time and building name as the text. 
        /// </returns>
        private static IEnumerable<SelectListItem> GetSelectList(List<EventData> events)
        {
            return events.Select(e => new SelectListItem { Value = e.ProgramEventGuid.ToString(), Text = e.GetEventInfo() }).ToList();
        }

        /// <summary>
        /// This method passes an Registration Data record to the API to insert the record into the Registration Table in the Event
        /// Registration database. 
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="RegistrationData"/>.
        /// </returns>
        private RegistrationData AddRegistration(Form data)
        {
            this.logger.Trace($"Calling Interface.AddNewRegistration from public form");
            Dictionary<string, string> detailList = new Dictionary<string, string>();
            detailList.Add("School Name", data.SchoolName);
            detailList.Add("Grad Year", data.GradYear.ToString());

            RegistrationData reg = new RegistrationData
                                       {
                                           FirstName = data.FirstName,
                                           LastName = data.LastName,
                                           Address = data.Address,
                                           City = data.City,
                                           State = data.State,
                                           Zipcode = data.Zipcode,
                                           EmailAddress = data.EmailAddress,
                                           Details = JsonConvert.SerializeObject(detailList),
                                           PhoneNumber = data.PhoneNumber,
                                           NumberAttending = data.NbrAttending,
                                           ProgramEventGuid = Guid.Parse(data.SelectedEvent),
                                           DuplicateCheck = false
                                       };

            // todo: change this to return a status flag, message and data
            // returns a registration data record with ID, GUID,
            var response = Interface.AddNewRegistration(EventRegistrationId, reg);
           // RegistrationData newReg = Interface.AddNewRegistration(EventRegistrationId, reg);
          // ADD CODE TO CHECK FOR A DUP ERROR
           //if (!good reponse)
           // ?SEt error 

            return response;
        }

        /// <summary>
        /// This method calls the API to validate the user input. 
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// true means a registration exists for this email address and event. 
        /// </returns>
        private bool CheckDuplicate(Form data)
        {
            Guid programEventGuid = Guid.Parse(data.SelectedEvent);
            this.logger.Trace($"Calling Interface.GetRegistrations to get all registrations for event {programEventGuid}");
            // todo: invoke the api to return duplicate flag
            // List<RegistrationData> list = Interface.GetRegistrations(EventRegistrationId, programEventGuid.ToString()).ToList();
            return Interface.DuplicateRegistration(EventRegistrationId, programEventGuid.ToString(), data.EmailAddress);
        }

        /// <summary>
        /// The get campus email.
        /// </summary>
        /// <param name="campus">
        /// The campus.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetCampusEmail(string campus)
        {
            switch (campus)
            {
                case "M":
                case "U":
                    return Properties.Settings.Default.MetroEmail;

                case "E":
                    return Properties.Settings.Default.EastEmail;

                case "B":
                case "W":
                    return Properties.Settings.Default.WestEmail;

                case "S":
                    return Properties.Settings.Default.WestshoreEmail;

                default:
                    return Properties.Settings.Default.MetroEmail;
            }
        }

        /// <summary>
        /// This method calls the Google ReCaptcha API..
        /// </summary>
        /// <param name="errorMsgId">The id of the form field where the error message should be displayed.</param>
        private void VerifyCaptcha(string errorMsgId)
        {
            const string CaptchaError = "The ReCaptcha checkbox is required.";
            RecaptchaVerificationHelper recaptchaHelper = this.GetRecaptchaVerificationHelper();

            if (string.IsNullOrEmpty(recaptchaHelper.Response))
            {
                this.ModelState.AddModelError(errorMsgId, CaptchaError);
                return;
            }

            RecaptchaVerificationResult recaptchaResult = recaptchaHelper.VerifyRecaptchaResponse();
            if (recaptchaResult != RecaptchaVerificationResult.Success)
            {
                this.ModelState.AddModelError(errorMsgId, CaptchaError);
            }
        }
    }
}