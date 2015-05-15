using RoomUsageApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoomUsageApp.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            ReportChartByTypeModel model = new ReportChartByTypeModel();
            model.BuildingNo = "320005";
            model.QueryReportAllFaculty();
            return View();
        }
    }
}