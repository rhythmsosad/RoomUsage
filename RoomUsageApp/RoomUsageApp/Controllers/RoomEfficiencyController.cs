using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoomUsageApp.Models;

namespace RoomUsageApp.Controllers
{
    public class RoomEfficiencyController : Controller
    {
        // GET: RoomEfficiency
        public ActionResult Index()
        {
            ReportChartByTypeModel model = new ReportChartByTypeModel();
            model.QueryReportAllFaculty();
            return View(model);
        }
    }
}