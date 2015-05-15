using RoomUsageApp.Models;
using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoomUsageApp.Controllers
{
    public class BuildingController : Controller
    {
        // GET: Building
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BuildingLut()
        {
            BuildingModel model = new BuildingModel();
            List<Building> result = model.QueryLutBuilding();
            return Json(result.Select(o => new { BuildingNo = o.BuildingNo, Name = o.NameTh + " (" + o.NameEn + ")" }));
        }

        public ActionResult BuildingLutByFaculty(string facultyCode)
        {
            BuildingModel model = new BuildingModel();
            List<Building> result = model.QueryLutBuildingByFaculty(facultyCode);
            return Json(result.Select(o => new { BuildingNo = o.BuildingNo, Name = o.NameTh + " (" + o.NameEn + ")" }));
        }
    }
}