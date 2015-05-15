using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RoomUsageApp.Models
{
    public class ReportChartByTypeModel : ReportModelBase
    {
        public void QueryReportAllFaculty()
        {
            using (var entities = new DB_CHINEntities())
            {
                
                var roomByFac = from tRoom in entities.Room
                                join tBuilding in entities.Building on tRoom.BuildingNo equals tBuilding.BuildingNo
                                join tFaculty in entities.Faculty on tBuilding.FacultyCode equals tFaculty.Code
                                group tRoom by new { tBuilding.FacultyCode, tFaculty.Name } into tTable
                                select new
                                {
                                    FacultyCode = tTable.Key.FacultyCode,
                                    FacultyName = tTable.Key.Name,
                                    HourAvailable = tTable.Count() * 35
                                };

                var used = from tTime in entities.ScheduleTime
                           join tRoom in entities.Room on tTime.RoomId equals tRoom.Id
                           join tBuilding in entities.Building on tRoom.BuildingNo equals tBuilding.BuildingNo
                           join tFaculty in entities.Faculty on tBuilding.FacultyCode equals tFaculty.Code
                           where tTime.StartTime != null &&
                               tTime.EndTime != null &&
                               tTime.RoomId != null
                           select new ReportChartByTypeModelItem()
                           {
                               BuildingName = tBuilding.NameTh + " (" + tBuilding.NameEn + ")",
                               BuildingNo = tRoom.BuildingNo,
                               EndTime = tTime.EndTime,
                               FacultyCode = tBuilding.FacultyCode,
                               FaculyName = tFaculty.Name,
                               RoomNo = tRoom.RoomNo,
                               StartTime = tTime.StartTime,
                               ScheduleId = tTime.Id
                           };
                List<ReportChartByTypeModelItem> usedList = used.ToList();

                //usedList.ToList().ForEach(o => o.HoursUsed = GetTimeSpan(o.EndTime).Subtract(GetTimeSpan(o.StartTime)).TotalHours);
                int i;
                for(i = 0; i < usedList.Count; i++)
                {
                    string endTime = usedList[i].EndTime;
                    string startTime = usedList[i].StartTime;

                    usedList[i].HoursUsed = GetTimeSpan(endTime, startTime).TotalHours;
                }
                
                // By Faculty
                List<ReportChartByFaculty> summaryList = roomByFac.ToList().Select(o => new ReportChartByFaculty() {
                    FacultyCode = o.FacultyCode,
                    FaculyName = o.FacultyName,
                    HoursAvailable = o.HourAvailable,
                    HoursUsed = usedList.Where(x => x.FacultyCode == o.FacultyCode).Select(x => x.HoursUsed).Sum()
                }).ToList();
            }
        }

        private TimeSpan GetTimeSpan(string strEndDate, string strStartDate)
        {
            strEndDate = strEndDate.Trim();
            strStartDate = strStartDate.Trim();

            int minuteEnd = int.Parse(strEndDate.Substring(strEndDate.Length - 2, 2));
            int hourEnd = int.Parse(strEndDate.Substring(0, strEndDate.Length - 2));

            int minuteStart = int.Parse(strStartDate.Substring(strStartDate.Length - 2, 2));
            int hourStart = int.Parse(strStartDate.Substring(0, strStartDate.Length - 2));


            DateTime today = DateTime.Now;
            DateTime dtEnd = new DateTime(today.Year, today.Month, today.Day, hourEnd, minuteEnd, 0);
            DateTime dtStart = new DateTime(today.Year, today.Month, today.Day, hourStart, minuteStart, 0);

            TimeSpan ts = dtEnd.Subtract(dtStart);
            return ts;
        }
    }

    public class ReportChartByFaculty
    {
        public string FacultyCode { get; set; }
        public string FaculyName { get; set; }

        public double HoursUsed { get; set; }
        public double HoursAvailable { get; set; }
        public double HoursPercent { get; set; }
    }

    public class ReportChartByTypeModelItem
    {
        public string FacultyCode { get; set; }
        public string FaculyName { get; set; }
        public string BuildingNo { get; set; }
        public string BuildingName { get; set; }
        public string RoomNo { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public double HoursUsed { get; set; }

        public long ScheduleId { get; set; }
    }
}