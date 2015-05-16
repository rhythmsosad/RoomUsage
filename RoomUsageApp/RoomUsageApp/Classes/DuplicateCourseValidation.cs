using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data; // require อันนี้เสมอ เพราะจะใช้ .AsEnumurable

namespace RoomUsageApp.Classes
{
    public class DuplicateCourseValidation : ValidationBase
    {
        // สมมติคลาสนี้จะเช็คว่า Course Code ห้ามเกิน 6 หลัก (สมมตินะ)
        public override bool IsValid()
        {
            var record = (from o in data.AsEnumerable()
                       select new
                       {
                           CourseCode = o["COURSECODE"].ToString(),
                           NameEngAbbr = o["NAMEENGABBR"].ToString(),
                           FacultyCode = o["FACCODE"].ToString()
                       }).Distinct();

            var duplicate = from tRecord in record
                            group tRecord by tRecord.CourseCode into tTable
                            where tTable.Count() > 1
                            select new
                            {
                                CourseCode = tTable.Key,
                                CountCourse = tTable.Count()
                            };
            if(duplicate.Count() > 0)
            {
                Message = string.Format("COURSECODE เหล่านี้ มีชื่อ Course ไม่เหมือนกัน: {0}", string.Join(", ", duplicate.Select(o => o.CourseCode).ToArray()));
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}