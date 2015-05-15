using RoomUsageApp.Classes;
using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RoomUsageApp.Models
{
    public class ReportTableModel : ReportModelBase
    {
        
        public List<string> ReportHeader { get; set; }
        public List<List<object>> ReportData { get; set; }

        public void QueryReport()
        {
            // 320005
            using (var entities = new DB_CHINEntities())
            {
                int hour = 0;
                List<ReportTimePeriod> timeSpan = new List<ReportTimePeriod>();
                for (hour = 8; hour < 21; hour++)
                {
                    timeSpan.Add(new ReportTimePeriod() { From = hour * 100, To = (hour * 100) + 30 });
                    timeSpan.Add(new ReportTimePeriod() { From = (hour * 100) + 30, To = (hour + 1) * 100 });
                }
                List<string> days = new List<string>() { "MO", "TU", "WE", "TH", "FR", "SA", "SU" };
                //List<string> days = new List<string>() { "MO" };

                if (Days != null && Days.Length > 0)
                {
                    days = Days.ToList();
                }

                DataTable dt = new DataTable();

                var allRoom = from tST in entities.ScheduleTime
                              join tRoom in entities.Room on tST.RoomId equals tRoom.Id
                              where tRoom.BuildingNo == BuildingNo &&
                                (string.IsNullOrEmpty(RoomNo) || tRoom.RoomNo == RoomNo)
                              select new { RoomId = tST.RoomId, RoomNo = tRoom.RoomNo };

                dt.Columns.Add("Day");
                dt.Columns.Add("Time");
                dt.Columns.AddRange(allRoom.Select(r => new DataColumn() { ColumnName = r.RoomNo }).Distinct().ToArray());


                var result = from tST in entities.ScheduleTime
                             join tRoom in entities.Room on tST.RoomId equals tRoom.Id
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
                                 Section = tS.Section
                             };

                List<ScheduleItem> resultList = result.ToList().Where(o =>
                                (string.IsNullOrEmpty(StartTime) || int.Parse(o.StartTime.Replace(":", "")) > int.Parse(StartTime.Replace(":", ""))) &&
                                (string.IsNullOrEmpty(EndTime) || int.Parse(o.EndTime.Replace(":", "")) > int.Parse(EndTime.Replace(":", "")))).ToList();

                foreach (string d in days)
                {
                    foreach (ReportTimePeriod p in timeSpan)
                    {
                        if(resultList.Where(o => o.Day == d).Count() > 0)
                        {
                            dt.Rows.Add(d, string.Format("{0}-{1}", p.From, p.To));
                            foreach (ScheduleItem si in resultList.Where(o => o.Day == d && (p.From >= int.Parse(o.StartTime) && p.From <= int.Parse(o.EndTime)) && (p.To >= int.Parse(o.StartTime) && p.To <= int.Parse(o.EndTime))))
                            {
                                dt.Rows[dt.Rows.Count - 1][si.RoomNo] = si.CourseCode.Trim() + " Sec. " + si.Section;
                            }
                        }
                    }
                }

                ReportHeader = new List<string>();
                foreach(DataColumn dc in dt.Columns)
                {
                    ReportHeader.Add(dc.ColumnName);
                }

                ReportData = dt.AsEnumerable().Select(o => o.ItemArray.ToList()).ToList();

                //return dt;
            }
        }
    }
}