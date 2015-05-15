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
            //ReportChartByTypeModel model = new ReportChartByTypeModel();
            //model.BuildingNo = "320005";
            //model.QueryReportAllFaculty();
            //return View();


            ReportTableModel model = new ReportTableModel();
            model.Days = new string[] { "MO", "TU", "WE", "TH", "FR", "SA", "SU" };
            model.BuildingNo = "320005";
            model.StartTime = "08:00";
            model.EndTime = "22:00";
            model.QueryReport();
            return View();
        }
    }
}