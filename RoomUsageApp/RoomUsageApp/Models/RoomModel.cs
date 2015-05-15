using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomUsageApp.Models
{
    public class RoomModel
    {
        public List<Room> QueryLutRoom(string buildingNo)
        {
            using (var entities = new DB_CHINEntities())
            {
                var item = from o in entities.Room
                           where o.BuildingNo == buildingNo
                           select o;

                return item.ToList();
            }
        }
    }
}