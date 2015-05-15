using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomUsageApp.Models
{
    public class ReportTableModel
    {
        string BuildingNo { get; set; }
        string RoomNo { get; set; }
        string[] Days { get; set; }
        string StartTime { get; set; }
        string EndTime { get; set; }
    }
}