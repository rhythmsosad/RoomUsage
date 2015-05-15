using RoomUsageApp.Models;
using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoomUsageApp.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RoomLut(string buildingNo)
        {
            RoomModel model = new RoomModel();
            List<Room> result = model.QueryLutRoom(buildingNo);
            return Json(result.Select(o => new { RoomId = o.Id, RoomNo = o.RoomNo }));
        }
    }
}