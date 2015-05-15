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

        public string Labels { get; set; }
        public string DataHours { get; set; }
        public string DataSeat { get; set; }

        public void QueryReportAllFaculty()
        {
            using (var entities = new DB_CHINEntities())
            {
                // Hour Used
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

                int i;
                for (i = 0; i < usedList.Count; i++)
                {
                    string endTime = usedList[i].EndTime;
                    string startTime = usedList[i].StartTime;

                    usedList[i].HoursUsed = GetTimeSpan(endTime, startTime).TotalHours;
                }

                List<ReportChartByFaculty_Hour> summaryList = roomByFac.ToList().Select(o => new ReportChartByFaculty_Hour()
                {
                    FacultyCode = o.FacultyCode,
                    FaculyName = o.FacultyName,
                    HoursAvailable = o.HourAvailable,
                    HoursUsed = usedList.Where(x => x.FacultyCode == o.FacultyCode).Select(x => x.HoursUsed).Sum(),
                    HoursRatio = Math.Round((double)usedList.Where(x => x.FacultyCode == o.FacultyCode).Select(x => x.HoursUsed).Sum() / (double)o.HourAvailable, 2),
                    HoursPercent = Math.Round(((double)usedList.Where(x => x.FacultyCode == o.FacultyCode).Select(x => x.HoursUsed).Sum() / (double)o.HourAvailable) * 100, 2)
                }).ToList();


                // Room Capability
                var capByFac = from tTime in entities.ScheduleTime
                               join tSchedule in entities.Schedule on tTime.ScheduleId equals tSchedule.Id
                               join tRoom in entities.Room on tTime.RoomId equals tRoom.Id
                               join tBuilding in entities.Building on tRoom.BuildingNo equals tBuilding.BuildingNo
                               join tFaculty in entities.Faculty on tBuilding.FacultyCode equals tFaculty.Code
                               where tTime.StartTime != null &&
                                   tTime.EndTime != null &&
                                   tTime.RoomId != null
                               group tRoom by new { tBuilding.FacultyCode, tFaculty.Name } into tTable
                               select new
                               {
                                   FacultyCode = tTable.Key.FacultyCode,
                                   FacultyName = tTable.Key.Name,
                                   SeatAvailable = tTable.Sum(o => o.NumClassSeat)
                               };
                capByFac.ToList();

                var capUsedByFac = from tTime in entities.ScheduleTime
                                   join tSchedule in entities.Schedule on tTime.ScheduleId equals tSchedule.Id
                                   join tRoom in entities.Room on tTime.RoomId equals tRoom.Id
                                   join tBuilding in entities.Building on tRoom.BuildingNo equals tBuilding.BuildingNo
                                   join tFaculty in entities.Faculty on tBuilding.FacultyCode equals tFaculty.Code
                                   where tTime.StartTime != null &&
                                       tTime.EndTime != null &&
                                       tTime.RoomId != null
                                   group tSchedule by new { tBuilding.FacultyCode, tFaculty.Name } into tTable
                                   select new
                                   {
                                       FacultyCode = tTable.Key.FacultyCode,
                                       FacultyName = tTable.Key.Name,
                                       SeatUsed = tTable.Sum(o => o.RealReg)
                                   };
                capUsedByFac.ToList();

                var capSummary = from tAvailable in capByFac
                                 join tUsed in capUsedByFac on tAvailable.FacultyCode equals tUsed.FacultyCode
                                 select new ReportChartByFaculty_Seat()
                                 {
                                     FacultyCode = tAvailable.FacultyCode,
                                     FaculyName = tAvailable.FacultyName,
                                     SeatAvailable = tAvailable.SeatAvailable.Value,
                                     SeatUsed = tUsed.SeatUsed.Value,
                                     SeatRatio = Math.Round((double)tUsed.SeatUsed.Value / (double)tAvailable.SeatAvailable.Value, 2),
                                     SeatPercent = Math.Round(((double)tUsed.SeatUsed.Value / (double)tAvailable.SeatAvailable.Value) * 100, 2)
                                 };
                List<ReportChartByFaculty_Seat> capSummaryList = capSummary.ToList();

                List<Faculty> allFaculty = entities.Faculty.ToList();

                var allSummary = from tFac in allFaculty
                                 join tHour in summaryList on tFac.Code equals tHour.FacultyCode
                                 join tSeat in capSummaryList on tFac.Code equals tSeat.FacultyCode
                                 select new ReportChartByFaculty_Summary()
                                 {
                                     FacultyCode = tFac.Code,
                                     FaculyName = tFac.Name,
                                     HoursPercent = tHour.HoursPercent,
                                     SeatPercent = tSeat.SeatPercent
                                 };

                Labels = string.Format("[{0}]", string.Join(", ", allSummary.Select(o => string.Format("\"{0}\"", o.FaculyName).ToArray()));
                DataHours = string.Format("[{0}]", string.Join(", ", allSummary.Select(o => o.HoursPercent).ToArray()));
                DataSeat = string.Format("[{0}]", string.Join(", ", allSummary.Select(o => o.SeatPercent).ToArray()));
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

    public class ReportChartByFaculty_Hour
    {
        public string FacultyCode { get; set; }
        public string FaculyName { get; set; }

        public double HoursUsed { get; set; }
        public double HoursAvailable { get; set; }
        public double HoursPercent { get; set; }
        public double HoursRatio { get; set; }
    }

    public class ReportChartByFaculty_Seat
    {
        public string FacultyCode { get; set; }
        public string FaculyName { get; set; }

        public double SeatUsed { get; set; }
        public double SeatAvailable { get; set; }
        public double SeatPercent { get; set; }
        public double SeatRatio { get; set; }
    }

    public class ReportChartByFaculty_Summary
    {
        public string FacultyCode { get; set; }
        public string FaculyName { get; set; }

        public double HoursPercent { get; set; }
        public double SeatPercent { get; set; }
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