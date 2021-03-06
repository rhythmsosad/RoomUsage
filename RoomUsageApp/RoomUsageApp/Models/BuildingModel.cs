﻿using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomUsageApp.Models
{
    public class BuildingModel
    {
        public List<Building> QueryLutBuilding()
        {
            using (var entities = new DB_CHINEntities())
            {
                var item = from o in entities.Building
                           select o;

                return item.ToList();
            }
        }

        public List<Building> QueryLutBuildingByFaculty(string facultyCode)
        {
            using (var entities = new DB_CHINEntities())
            {
                var item = from o in entities.Building
                           where o.FacultyCode == facultyCode
                           select o;

                return item.ToList();
            }
        }
    }
}