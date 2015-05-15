using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomUsageApp.Models
{
    public class ReportModelBase
    {
        public string BuildingNo { get; set; }
        public string RoomNo { get; set; }
        public string[] Days { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}