using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoomUsageApp.Models;

namespace RoomUsageApp.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Table()
        {
            //return Json(new {  recordsTotal = 2, recordsFiltered = 2, data = new List<object>() { new { first_name = "first111", last_name = "last111" }, new { first_name = "first222", last_name = "last222" } } });

            ReportTableModel queryModel = new ReportTableModel();
            //List<ReportTableModel> response = queryModel.QueryReport();
            queryModel.QueryReport();
            return View(queryModel);
        }
    }
}