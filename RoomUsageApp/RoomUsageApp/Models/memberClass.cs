using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RoomUsageApp.Models
{
    public class memberClass
    {
        public string name { get; set; }
        public string surname { get; set; }
        // public var surname { get; set; }
        public List<Faculty> testList { get; set; }
        public void Query()
        {
            using (var entity = new DB_CHINEntities())
            {
                var obj = from o in entity.Faculty
                          select o;
                testList = obj.ToList();
            }

  
        }
        
    }
}