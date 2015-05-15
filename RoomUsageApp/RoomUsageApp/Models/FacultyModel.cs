using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomUsageApp.Models
{
    public class FacultyModel
    {
        public List<Faculty> QueryLutFaculty()
        {
            using (var entities = new DB_CHINEntities())
            {
                var item = from o in entities.Faculty
                           select o;

                return item.ToList();
            }
        }
    }
}