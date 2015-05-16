using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomUsageApp.Models
{
    public class ScheduleItem
    {
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string RoomNo { get; set; }
        public string CourseCode { get; set; }
        public short? Section { get; set; }
        public string BuildingNo { get; set; }
        public string BuildingFacCode { get; set; }
        public string BuildingFacName { get; set; }
        public short? Seat { get; set; }
    }
}