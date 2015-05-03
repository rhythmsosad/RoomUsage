using RoomUsageApp.Models;
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
            model.name = "Name";
            model.surname = "Surname";
            return View(model);
        }

        //tests tons
    }
}