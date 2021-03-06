﻿using RoomUsageApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoomUsageApp.Controllers
{
    public class Test2Controller : Controller
    {
        // GET: Test2
        public ActionResult Index()
        {
            Class1 model = new Class1();

            List<Class1> data1 = model.GetName();
            List<Class1> data2 = model.GetSalary();

            // where
            var result = data1.OrderBy(x => x.name).Where(x => x.name == "name1").Select(x => x.salary).Sum();

            var oResult = (from o in data1
                          where o.name == "name1"
                          orderby o.name
                          select o.salary).Sum();

            // chin

            model.name = "Name";
            model.surname = "Surname";
            return View(model);
        }

        //tests tons
    }
}