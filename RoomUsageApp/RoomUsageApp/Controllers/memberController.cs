using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoomUsageApp.Models;

namespace RoomUsageApp.Controllers
{
    public class memberController : Controller
    {
        // GET: ton
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult testPage01()
        {
            memberClass model = new memberClass();
          
            model.name = "TestName";
            model.surname = "TestSurname";

           // model.testList

            return View(model);
        }
    }
}