using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoomUsageApp.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Table()
        {
            return Json(new { data = new List<object>() { new { first_name = "first111", last_name = "last111" }, new { first_name = "first222", last_name = "last222" } } });

            //AdminUser queryModel = new AdminUser();
            //List<AdminUserItem> response = queryModel.Query();
            //return Json(response);
        }
    }
}