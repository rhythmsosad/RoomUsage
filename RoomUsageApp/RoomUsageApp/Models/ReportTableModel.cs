﻿using RoomUsageApp.Classes;
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
                              select new { RoomId = tST.RoomId, RoomNo = tRoom.RoomNo, Seat = tRoom.NumClassSeat };

                dt.Columns.Add("Day");
                dt.Columns.Add("Time");
                dt.Columns.AddRange(allRoom.Select(r => new DataColumn() { ColumnName = !string.IsNullOrEmpty(r.RoomNo) ? (r.RoomNo + "<br />" + r.Seat + " ที่นั่ง") : "ไม่ระบุห้อง" }).Distinct().ToArray());


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
                                 Section = tS.Section,
                                 Seat = tRoom.NumClassSeat
                             };

                //result.ToList();

                //List<ScheduleItem> resultList = result.ToList().Where(o =>
                //                (string.IsNullOrEmpty(StartTime) || int.Parse(o.StartTime.Replace(":", "")) > int.Parse(StartTime.Replace(":", ""))) &&
                //                (string.IsNullOrEmpty(EndTime) || int.Parse(o.EndTime.Replace(":", "")) > int.Parse(EndTime.Replace(":", "")))).ToList();

                List<ScheduleItem> resultList = result.ToList();

                foreach (string d in days)
                {
                    foreach (ReportTimePeriod p in timeSpan)
                    {
                        if(resultList.Where(o => o.Day == d).Count() > 0)
                        {
                            dt.Rows.Add(d, GetTimeString(p.From, p.To));
                            foreach (ScheduleItem si in resultList.Where(o => o.Day == d && (p.From >= int.Parse(o.StartTime) && p.From <= int.Parse(o.EndTime)) && (p.To >= int.Parse(o.StartTime) && p.To <= int.Parse(o.EndTime))))
                            {
                                if(!dt.Rows[dt.Rows.Count - 1][!string.IsNullOrEmpty(si.RoomNo) ? (si.RoomNo + "<br />" + si.Seat + " ที่นั่ง") : "ไม่ระบุห้อง"].ToString().Contains(si.CourseCode.Trim() + " Sec. " + si.Section))
                                {
                                    dt.Rows[dt.Rows.Count - 1][!string.IsNullOrEmpty(si.RoomNo) ? (si.RoomNo + "<br />" + si.Seat + " ที่นั่ง") : "ไม่ระบุห้อง"] += si.CourseCode.Trim() + " Sec. " + si.Section + "<hr />";
                                }
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

        private string GetTimeString(int from, int to)
        {
            string strFrom = from.ToString();
            string endFrom = to.ToString();

            int minuteEnd = int.Parse(endFrom.Substring(endFrom.Length - 2, 2));
            int hourEnd = int.Parse(endFrom.Substring(0, endFrom.Length - 2));

            int minuteStart = int.Parse(strFrom.Substring(strFrom.Length - 2, 2));
            int hourStart = int.Parse(strFrom.Substring(0, strFrom.Length - 2));

            return string.Format("{0}:{1} - {2}:{3}", hourStart.ToString().PadLeft(2, '0'), minuteStart.ToString().PadRight(2, '0'), hourEnd.ToString().PadLeft(2, '0'), minuteEnd.ToString().PadRight(2, '0'));
        }

    }
}