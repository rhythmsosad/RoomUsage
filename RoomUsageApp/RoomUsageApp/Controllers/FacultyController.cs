using RoomUsageApp.Models;
using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoomUsageApp.Controllers
{
    public class FacultyController : Controller
    {
        // GET: Faculty
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FacultyLut()
        {
            FacultyModel model = new FacultyModel();
            List<Faculty> result = model.QueryLutFaculty();
            return Json(result.Select(o => new { FacultyCode = o.Code, Name = o.Name }));
        }
    }
}