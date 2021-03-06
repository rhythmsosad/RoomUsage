﻿using System;
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
        //[OutputCache(Duration = 600)]
        public ActionResult Index()
        {
            ReportChartByTypeModel model = new ReportChartByTypeModel();
            model.QueryReportAllFaculty();
            return View(model);
        }

        [HttpPost]
        //[OutputCache(Duration = 600)]
        public ActionResult Index(ReportChartByTypeModel model)
        {
            if(!string.IsNullOrEmpty(model.BU))
            {
                model.QueryReportByBuilding(model.FC, model.BU);
            }
            else if(!string.IsNullOrEmpty(model.FC))
            {
                model.QueryReportByFaculty(model.FC);
            }
            else
            {
                model.QueryReportAllFaculty();
            }
            return View(model);
        }
    }
}