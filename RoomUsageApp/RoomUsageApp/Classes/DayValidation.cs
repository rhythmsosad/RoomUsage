using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data; // require อันนี้เสมอ เพราะจะใช้ .AsEnumurable

namespace RoomUsageApp.Classes
{
    public class DayValidation : ValidationBase
    {
        public override bool IsValid()
        {

            Boolean validateFlag = false;

            //// validate DAY1-7 Start
             int day1ValidateCount = data.AsEnumerable().Where(o => o["DAY1"].ToString() != ("") 
             && o["DAY1"].ToString() != ("MO") 
             && o["DAY1"].ToString() != ("TU")
             && o["DAY1"].ToString() != ("WE")
             && o["DAY1"].ToString() != ("TH")
             && o["DAY1"].ToString() != ("FR")
             && o["DAY1"].ToString() != ("SA")
             && o["DAY1"].ToString() != ("SU")).Count();

            int day2ValidateCount = data.AsEnumerable().Where(o => o["DAY2"].ToString() != ("")
            && o["DAY2"].ToString() != ("MO")
            && o["DAY2"].ToString() != ("TU")
            && o["DAY2"].ToString() != ("WE")
            && o["DAY2"].ToString() != ("TH")
            && o["DAY2"].ToString() != ("FR")
            && o["DAY2"].ToString() != ("SA")
            && o["DAY2"].ToString() != ("SU")).Count();

            int day3ValidateCount = data.AsEnumerable().Where(o => o["DAY3"].ToString() != ("")
            && o["DAY3"].ToString() != ("MO")
            && o["DAY3"].ToString() != ("TU")
            && o["DAY3"].ToString() != ("WE")
            && o["DAY3"].ToString() != ("TH")
            && o["DAY3"].ToString() != ("FR")
            && o["DAY3"].ToString() != ("SA")
            && o["DAY3"].ToString() != ("SU")).Count();

            int day4ValidateCount = data.AsEnumerable().Where(o => o["DAY4"].ToString() != ("")
            && o["DAY4"].ToString() != ("MO")
            && o["DAY4"].ToString() != ("TU")
            && o["DAY4"].ToString() != ("WE")
            && o["DAY4"].ToString() != ("TH")
            && o["DAY4"].ToString() != ("FR")
            && o["DAY4"].ToString() != ("SA")
            && o["DAY4"].ToString() != ("SU")).Count();

            int day5ValidateCount = data.AsEnumerable().Where(o => o["DAY5"].ToString() != ("")
            && o["DAY5"].ToString() != ("MO")
            && o["DAY5"].ToString() != ("TU")
            && o["DAY5"].ToString() != ("WE")
            && o["DAY5"].ToString() != ("TH")
            && o["DAY5"].ToString() != ("FR")
            && o["DAY5"].ToString() != ("SA")
            && o["DAY5"].ToString() != ("SU")).Count();

            int day6ValidateCount = data.AsEnumerable().Where(o => o["DAY6"].ToString() != ("")
            && o["DAY6"].ToString() != ("MO")
            && o["DAY6"].ToString() != ("TU")
            && o["DAY6"].ToString() != ("WE")
            && o["DAY6"].ToString() != ("TH")
            && o["DAY6"].ToString() != ("FR")
            && o["DAY6"].ToString() != ("SA")
            && o["DAY6"].ToString() != ("SU")).Count();

            int day7ValidateCount = data.AsEnumerable().Where(o => o["DAY7"].ToString() != ("")
            && o["DAY7"].ToString() != ("MO")
            && o["DAY7"].ToString() != ("TU")
            && o["DAY7"].ToString() != ("WE")
            && o["DAY7"].ToString() != ("TH")
            && o["DAY7"].ToString() != ("FR")
            && o["DAY7"].ToString() != ("SA")
            && o["DAY7"].ToString() != ("SU")).Count();
            ///////////// end validate DAY1-7

            //////////// validate SEMESTER 1 2 3

            int semesterValidateCount = data.AsEnumerable().Where(o => o["SEMESTER"].ToString() != ("")
            && o["SEMESTER"].ToString() != ("1")
            && o["SEMESTER"].ToString() != ("2")
            && o["SEMESTER"].ToString() != ("3")).Count();

            /////////////// end validate SEMESTER
            Message = "";
            if (day1ValidateCount > 0)
            {
                Message += string.Format("<br>มีแถวใน Column DAY1 ที่ไม่ใช่ SU,MO,TU,WE,TH,FR,SA จำนวน {0} แถว กรุณาตรวจสอบและนำเข้าอีกครั้ง", day1ValidateCount );
                validateFlag = true;
                  
            }
            if (day2ValidateCount > 0)
            {
                Message += string.Format("<br>มีแถวใน Column DAY2 ที่ไม่ใช่ SU,MO,TU,WE,TH,FR,SA จำนวน {0} แถว กรุณาตรวจสอบและนำเข้าอีกครั้ง", day2ValidateCount);
                validateFlag = true;
            }
            if (day3ValidateCount > 0)
            {
                Message += string.Format("<br>มีแถวใน Column DAY3 ที่ไม่ใช่ SU,MO,TU,WE,TH,FR,SA จำนวน {0} แถว กรุณาตรวจสอบและนำเข้าอีกครั้ง", day3ValidateCount );
                validateFlag = true;
            }
            if (day4ValidateCount > 0)
            {
                Message += string.Format("<br>มีแถวใน Column DAY4 ที่ไม่ใช่ SU,MO,TU,WE,TH,FR,SA จำนวน {0} แถว กรุณาตรวจสอบและนำเข้าอีกครั้ง", day4ValidateCount);
                validateFlag = true;
            }
            if (day5ValidateCount > 0)
            {
                Message += string.Format("<br>มีแถวใน Column DAY5 ที่ไม่ใช่ SU,MO,TU,WE,TH,FR,SA จำนวน {0} แถว กรุณาตรวจสอบและนำเข้าอีกครั้ง", day5ValidateCount);
                validateFlag = true;
            }
            if (day6ValidateCount > 0)
            {
                Message += string.Format("<br>มีแถวใน Column DAY6 ที่ไม่ใช่ SU,MO,TU,WE,TH,FR,SA จำนวน {0} แถว กรุณาตรวจสอบและนำเข้าอีกครั้ง", day6ValidateCount);
                validateFlag = true;
            }

            if (day7ValidateCount > 0)
            {
                Message += string.Format("<br>มีแถวใน Column DAY7 ที่ไม่ใช่ SU,MO,TU,WE,TH,FR,SA จำนวน {0} แถว กรุณาตรวจสอบและนำเข้าอีกครั้ง", day7ValidateCount);
                validateFlag = true;
            }

            if (semesterValidateCount > 0)
            {
                Message += string.Format("<br>มีแถวใน Column SEMETER ที่ไม่ใช่ 1,2,3 จำนวน {0} แถว กรุณาตรวจสอบและนำเข้าอีกครั้ง", semesterValidateCount);
                validateFlag = true;
            }
       

            var dateTime = DateTime.Now;
           // int count = data.AsEnumerable().Where(o => o["YEAR"].DateofBirth.MONTH == dateTime.Month ).Count();
           // int count1 = data.AsEnumerable().Where(o => DateTime.Parse(o["YEAR"].DateofBirth).Month == dateTime.Month).Count();
            //DateTime.Parse(o["YEAR"].DateofBirth).Month

            if (validateFlag)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}