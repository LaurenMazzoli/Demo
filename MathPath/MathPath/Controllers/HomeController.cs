// <copyright file="HomeController.cs" company="Cuyahoga Community College">
// Copyright (c) 2020 Cuyahoga Community College.  All rights reserved.
// </copyright>
// <summary>
// MathPathways displays a home screen where users choose a major and enter their placement information.
// Their placement for each math pathway for that major is calculated and displayed in the Results view. 
// </summary>
// <remarks>
// 
// </remarks>
// <source_repository>
// Current Revision : $Revision: 3 $
// Last Check In    : $Date: 2020-09-09 15:49:54-04:00 $
// Last Modification: $Modtime: 2020-09-04 21:35:27-04:00 $
// Last Change By   : $Author: lmazzol $
// $Log: /MathPath/MathPath/Controllers/HomeController.cs $
// 
// Revision: 3   Date: 2020-09-09 19:49:54Z   User: lmazzol 
// checking in remaining files 
// 
// Revision: 2   Date: 2020-09-05 01:29:37Z   User: lmazzol 
// Completed development 
// 
// Revision: 1   Date: 2020-08-04 01:16:18Z   User: lmazzol 
// </source_repository>
namespace MathPath.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using System.Web.Script.Serialization;

    using MathPath.DAL;
    using MathPath.Data;
    using MathPath.Models;
    using MathPath.Models.ViewModel;
    using MathPath.Properties;

    using Microsoft.Ajax.Utilities;

    using NLog;

    /// <summary>
    /// The home controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// The logger object.
        /// </summary>
        private readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The major change SQL database.
        /// </summary>
        private readonly Entities majorChange = new Entities(AppDbConnection.WebSqlFormsConn);

        /// <summary>
        /// The placement tests enumeration.
        /// </summary>
        public enum PlacementTests
        {
            /// <summary>
            /// The Accuplacer test.
            /// </summary>
            [SuppressMessage(
                "StyleCop.CSharp.DocumentationRules",
                "SA1650:ElementDocumentationMustBeSpelledCorrectly",
                Justification = "Reviewed. Suppression is OK here.")]
            AccuplacerNextGen = 1,

            /// <summary>
            /// The aleks test.
            /// </summary>
            [SuppressMessage(
                "StyleCop.CSharp.DocumentationRules",
                "SA1650:ElementDocumentationMustBeSpelledCorrectly",
                Justification = "Reviewed. Suppression is OK here.")]
            Aleks = 2,

            /// <summary>
            /// The ACT test.
            /// </summary>
            [SuppressMessage(
                "StyleCop.CSharp.DocumentationRules",
                "SA1650:ElementDocumentationMustBeSpelledCorrectly",
                Justification = "Reviewed. Suppression is OK here.")]
            ACT = 3,

            /// <summary>
            /// The SAT test.
            /// </summary>
            [SuppressMessage(
                "StyleCop.CSharp.DocumentationRules",
                "SA1650:ElementDocumentationMustBeSpelledCorrectly",
                Justification = "Reviewed. Suppression is OK here.")]
            SAT = 4
        }

        /// <summary>
        /// The placement score levels. Each level correlates to a placement test score range.
        /// </summary>
        public enum PlacementScores
        {
            /// <summary>
            /// The Red placement level
            /// </summary>
            Red = 1,

            /// <summary>
            /// The Orange placement level .
            /// </summary>
            Orange = 2,

            /// <summary>
            /// The Yellow placement level.
            /// </summary>
            Yellow = 3,

            /// <summary>
            /// The Green placement level.
            /// </summary>
            Green = 4,

            /// <summary>
            /// The Blue placement level.
            /// </summary>
            Blue = 5,

            /// <summary>
            /// The Purple placement level.
            /// </summary>
            Purple = 6,

            /// <summary>
            /// The Garnet placement level.
            /// </summary>
            Garnet = 7,

            /// <summary>
            /// The Gray placement level.
            /// </summary>
            Gray = 8,

            /// <summary>
            /// The Clear placement level.
            /// </summary>
            Clear = 99
        }

        /// <summary>
        /// The index method restores session variables if they exist, gets data for the view model and returns the index view.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index()
        {
            string major = this.HttpContext.Session["Major"] != null
                               ? (string)this.HttpContext.Session["Major"]
                               : string.Empty;

            PlacementTests placementTest = new PlacementTests();
            PlacementScores placementScore = new PlacementScores();

            if (this.HttpContext.Session["PlacementTest"] != null)
            {
                placementTest = (PlacementTests)this.HttpContext.Session["PlacementTest"];
            }

            if (this.HttpContext.Session["PlacementScore"] != null)
            {
                placementScore = (PlacementScores)this.HttpContext.Session["PlacementScore"];
            }

            List<SelectListItem> placementScoreList = placementTest != 0
                                                          ? this.GetScores(placementTest)
                                                          : new List<SelectListItem>();

            DataViewModel form = new DataViewModel
                                     {
                                         MajorList = this.GetMajors(),
                                         SelectedMajor = major,
                                         SelectedPlacementTest = placementTest.ToString(),
                                         PlacementScoreList = placementScoreList,
                                         SelectedPlacementScore = ((int)placementScore).ToString()
                                     };

            return this.View(form);
        }

        /// <summary>
        /// The post index method validates the data. If not valid, the correct errors are set. 
        /// If the data is valid, the selected data is stored to session variables.  The user is redirected to the index method.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [HttpPost]
        public ActionResult Index(DataViewModel data)
        {
            PlacementTests placementTest;
            Enum.TryParse(data.SelectedPlacementTest, out placementTest);
            if (!this.ModelState.IsValid)
            {
                data.MajorList = this.GetMajors();

                data.PlacementScoreList = data.SelectedPlacementTest != null
                                              ? this.GetScores(placementTest)
                                              : new List<SelectListItem>();
                return this.View(data);
            }

            this.HttpContext.Session["Major"] = data.SelectedMajor;
            this.HttpContext.Session["PlacementTest"] = placementTest;
            PlacementScores placementScore;
            Enum.TryParse(data.SelectedPlacementScore, out placementScore);
            this.HttpContext.Session["PlacementScore"] = placementScore;

            return this.View(data);
        }

        /// <summary>
        /// The error method returns the error view.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Error()
        {
            this.ViewBag.Message = "Error";
            return this.View();
        }

        /// <summary>
        /// The get placement scores method gets a dictionary of placement scores for the criteria entered. The dictionary is returned in the payload. 
        /// </summary>
        /// <param name="placementTest">
        /// The placement Test.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult GetPlacementScores(string placementTest)
        {
            PlacementTests test;
            Enum.TryParse(placementTest, out test);

            Dictionary<string, string> dictionary = this.GetScoreData(test);
            string data = new JavaScriptSerializer().Serialize(dictionary);
            return this.Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The get pathway results gets all the paths for the given criteria and returns them in the payload. 
        /// </summary>
        /// <param name="selectedMajor">
        /// The selected major.
        /// </param>
        /// <param name="selectedPlacementTest">
        /// The selected placement test.
        /// </param>
        /// <param name="selectedScore">
        /// The selected score.
        /// </param>
        /// <returns>
        /// The <see cref="JsonResult"/>.
        /// </returns>
        public JsonResult GetPathwayResults(string selectedMajor, string selectedPlacementTest, string selectedScore)
        {
            List<Major> majors = (List<Major>)this.HttpContext.Session["Majors"];

            int id = int.Parse(selectedMajor);
            Program program = this.majorChange.Programs.Find(id);
            if (program == null)
            {
                string error = new JavaScriptSerializer().Serialize(
                    new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Invalid Program found"));
                return this.Json(error, JsonRequestBehavior.DenyGet);
            }

            PlacementScores placementScore;
            PlacementTests placementTest;

            Enum.TryParse(selectedScore, out placementScore);
            Enum.TryParse(selectedPlacementTest, out placementTest);

            // get paths 
            List<PathModel> paths = this.GetPaths(program.ProgramPaths.ToList(), placementScore);
            int maxCourses = paths.Select(p => p.CourseList.Count).Max();
            ResultsViewModel results = new ResultsViewModel
                                           {
                                               Major = program.Description,
                                               Paths = paths,
                                               MaxNumCourses = maxCourses,
                                               Message = Settings.Default.ResultMessage
                                           };

            string data = new JavaScriptSerializer().Serialize(results);
            return this.Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The get scores method returns a list of select list items of Placement Scores for the given test type. 
        /// </summary>
        /// <param name="placementTest">
        /// The placement Test.
        /// </param>
        /// <returns>
        /// Placement Score drop down items.
        /// </returns>
        private List<SelectListItem> GetScores(PlacementTests placementTest)
        {
            Dictionary<string, string> dict = this.GetScoreData(placementTest);
            return dict.Select(item => new SelectListItem { Value = item.Key, Text = item.Value }).ToList();
        }

        /// <summary>
        /// The get score dictionary creates a dictionary used into create the score drop down list.
        /// </summary>
        /// <param name="placementTest">
        /// The placement test.
        /// </param>
        /// <returns>
        /// A dictionary of placement scores and placement levels.
        /// </returns>
        private Dictionary<string, string> GetScoreData(PlacementTests placementTest)
        {
            switch (placementTest)
            {
                case PlacementTests.AccuplacerNextGen:
                    {
                        return this.majorChange.Placements.Where(p => p.Accuplacer != null)
                            .OrderBy(p => p.PlacementLevel).ToDictionary(
                                p => p.PlacementLevel.ToString(),
                                p => p.Accuplacer);
                    }

                case PlacementTests.ACT:
                    {
                        return this.majorChange.Placements.Where(p => p.Act != null).OrderBy(p => p.PlacementLevel)
                            .ToDictionary(p => p.PlacementLevel.ToString(), p => p.Act);
                    }

                case PlacementTests.Aleks:
                    {
                        return this.majorChange.Placements.Where(p => p.Aleks != null).OrderBy(p => p.PlacementLevel)
                            .ToDictionary(p => p.PlacementLevel.ToString(), p => p.Aleks);
                    }

                case PlacementTests.SAT:
                    {
                        return this.majorChange.Placements.Where(p => p.Sat != null).OrderBy(p => p.PlacementLevel)
                            .ToDictionary(p => p.PlacementLevel.ToString(), p => p.Sat);
                    }

                default:
                    {
                        return new Dictionary<string, string>();
                    }
            }
        }

        /// <summary>
        /// Get majors gets a list of all programs. A Major object is created for each program. A list of majors is saved to session and used to
        /// create select list items used in the major dropdown. 
        /// </summary>
        /// <returns>
        /// A list of Select List Items for majors sorted by major name.
        /// </returns>
        private List<SelectListItem> GetMajors()
        {
            List<Major> list = new List<Major>();
            List<Program> programs = this.majorChange.Programs.ToList();

            foreach (Program program in programs)
            {
                list.Add(
                    new Major
                        {
                            Name = program.Description,
                            Type = string.Empty,
                            ProgramId = program.ProgramId,
                            ProgramCode = program.ProgramCode
                        });
            }

            int numPrograms = list.Count;

            // sort list by major name
            list = list.OrderBy(l => l.Name).ToList();
            this.HttpContext.Session["Majors"] = list;

            IEnumerable<SelectListItem> majors = list.Select(
                item => new SelectListItem
                            {
                                Value = item.ProgramId.ToString(), Text = $"{item.Name} ({item.ProgramCode})"
                            });
            return majors.ToList();
        }

        /// <summary>
        /// The Get Paths gets a list of potential paths for the given program, placement test and placement index.
        /// </summary>
        /// <param name="programPaths">
        /// The program Paths.
        /// </param>
        /// <param name="placementScore">
        /// The placement Score.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private List<PathModel> GetPaths(List<ProgramPath> programPaths, PlacementScores placementScore)
        {
            List<PathModel> list = new List<PathModel>();
            programPaths = programPaths.OrderBy(pp => pp.Path.Code).ToList();
            foreach (ProgramPath pp in programPaths)
            {
                Path path = pp.Path;
                List<Course> courses = this.GetCourseList(path.Courses);

                PathModel pathModel = new PathModel
                                          {
                                              ProgramPathName = pp.Name,
                                              PathName = path.Name,
                                              Code = path.Code,
                                              RequiredCourse = pp.RequiredCourse,
                                              CourseList = courses
                                          };

                int placementCourseId = this.SetPlacementCourse(path, courses, placementScore);
                if (placementCourseId < 0)
                {
                    continue; // skip this path
                }

                pathModel.PlacementCourseId = placementCourseId;
                pathModel.TestedOut = pathModel.PlacementCourseId == 0;
                list.Add(pathModel);
            }

            return list;
        }

        /// <summary>
        /// Get course list converts a string of comma delimited course numbers into a list.
        /// </summary>
        /// <param name="coursesString">
        /// The courses string.
        /// </param>
        /// <returns>
        /// The list of courses/>.
        /// </returns>
        private List<Course> GetCourseList(string coursesString)
        {
            coursesString = coursesString.Trim();
            coursesString = string.Concat(coursesString.Where(c => !char.IsWhiteSpace(c)));
            List<string> courseNumbers = coursesString.Split(',').ToList();

            List<Course> courseList = new List<Course>();
            foreach (string item in courseNumbers)
            {
                Course course = this.majorChange.Courses.FirstOrDefault(c => c.Number == item);
                courseList.Add(course);
            }

            return courseList;
        }

        /// <summary>
        /// The set placement course method determines the course placement for a given path. 
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="courses">
        /// The courses.
        /// </param>
        /// <param name="placementScore">
        /// The placement score.
        /// </param>
        /// <returns>
        /// zero means fulfilled, less than zero means no placement, greater than zero is the course id to be placed in.
        /// </returns>
        private int SetPlacementCourse(Path path, List<Course> courses, PlacementScores placementScore)
        {
            string code = path.Code;
            bool allowTestOut = path.AllowTestOut;
            int selectedPlacementLevel = (int)placementScore;
            int lastCourseId = -1;
            foreach (var course in courses)
            {
                if (course.PlacementLevel == selectedPlacementLevel)
                {
                    return course.CourseId;
                }

                if (course.PlacementLevel > selectedPlacementLevel)
                {
                    return lastCourseId;
                } 

                lastCourseId = course.CourseId;
            }

            return allowTestOut ? 0 : lastCourseId;
        }
    }
}