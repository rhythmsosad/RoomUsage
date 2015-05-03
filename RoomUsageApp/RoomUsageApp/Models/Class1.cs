using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomUsageApp.Models
{
    public class Class1
    {
        public string name { get; set; }
        public string surname { get; set; }

        public void Query() {
            using (var entity = new DB_CHINEntities())
            {
                var obj = from o in entity.Building
                          select o;
                obj.ToList();
            }
        }

    }
}