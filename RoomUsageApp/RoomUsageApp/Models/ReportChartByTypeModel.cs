using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoomUsageApp.Models
{
    public class ReportChartByTypeModel : ReportModelBase
    {
        public void QueryReport()
        {
            using (var entities = new DB_CHINEntities())
            {
                var result = from tST in entities.ScheduleTime
                             join tRoom in entities.Room on tST.RoomId equals tRoom.Id
                             join tBuilding in entities.Building on tRoom.BuildingNo equals tBuilding.BuildingNo
                             join tFac in entities.Faculty on tBuilding.FacultyCode equals tFac.Code
                             join tS in entities.Schedule on tST.ScheduleId equals tS.Id
                             where tRoom.BuildingNo == BuildingNo &&
                               tST.StartTime != null &&
                               tST.EndTime != null &&
                               tST.RoomId != null
                             select new ScheduleItem()
                             {
                                 Day = tST.Day,
                                 StartTime = tST.StartTime,
                                 EndTime = tST.EndTime,
                                 RoomNo = tRoom.RoomNo,
                                 CourseCode = tS.CourseCode,
                                 Section = tS.Section,
                                 BuildingNo = tRoom.BuildingNo,
                                 BuildingFacCode = tBuilding.FacultyCode,
                                 BuildingFacName = tFac.Name
                             };

                List<ScheduleItem> resultList = result.ToList();
            }
        }

        private TimeSpan GetTimeSpan(string str)
        {
            int minute = int.Parse(str.Substring(str.Length - 2, 2));
            int hour = int.Parse(str.Substring(0, str.Length - 2));
            TimeSpan ts = new TimeSpan(hour, minute, 0);
            return ts;
        }
    }
}