using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data; // require อันนี้เสมอ เพราะจะใช้ .AsEnumurable
using System.Text.RegularExpressions;

namespace RoomUsageApp.Classes
{
    public class teeValidation : ValidationBase
    {
        Regex NumberPattern = new Regex("^[0-9]+$");
        Regex YearPattern = new Regex("^d{4}?$");
        bool Valid;
        public override bool IsValid()
        {
            
            //Check CourseCode
            var queryCourseCode = data.AsEnumerable().Where(r => (r["COURSECODE"].ToString() !=""));
            if (queryCourseCode.Count() > 0)
            {
                DataTable Validate = queryCourseCode.CopyToDataTable();
                queryCourseCode = Validate.AsEnumerable().Where(
                    r => !(NumberPattern.IsMatch(r["COURSECODE"].ToString())));
               
            }
            //Check Realreg
            var queryRealreg = data.AsEnumerable().Where(r => (r["REALREG"].ToString() != ""));
            if (queryRealreg.Count() > 0)
            {
                DataTable Validate = queryRealreg.CopyToDataTable();
                queryRealreg = Validate.AsEnumerable().Where(
                    r => !(NumberPattern.IsMatch(r["REALREG"].ToString())));
            }
            //Check TotalReg
            var queryTotalReg = data.AsEnumerable().Where(r => (r["TOTALREG"].ToString() != ""));
            if (queryTotalReg.Count() > 0)
            {
                DataTable Validate = queryTotalReg.CopyToDataTable();
                queryTotalReg = Validate.AsEnumerable().Where(
                    r => !(NumberPattern.IsMatch(r["TOTALREG"].ToString())));
            }
            //Check Section
            var querySection = data.AsEnumerable().Where(r => (r["SECTION"].ToString() != ""));
            if (querySection.Count() > 0)
            {
                DataTable Validate = querySection.CopyToDataTable();
                querySection = Validate.AsEnumerable().Where(
                    r => !(NumberPattern.IsMatch(r["SECTION"].ToString())));
            }
            //Check Faccode
            var queryFaccode = data.AsEnumerable().Where(r => (r["FACCODE"].ToString() != ""));
            if (queryFaccode.Count() > 0)
            {
                DataTable Validate = queryFaccode.CopyToDataTable();
                queryFaccode = Validate.AsEnumerable().Where(
                    r => !(NumberPattern.IsMatch(r["FACCODE"].ToString())));
            }
              //Check Year 
            
             var queryYear = data.AsEnumerable().Where(r => (r["YEAR"].ToString() != ""));
             var queryYearPattern = data.AsEnumerable().Where(r => (r["YEAR"].ToString() != ""));
              if (queryYear.Count() > 0)
              {
                  DataTable Validate = queryYear.CopyToDataTable();
                  queryYear = Validate.AsEnumerable().Where(
                      r => !(NumberPattern.IsMatch(r["YEAR"].ToString())));
              }
              else
              {
                  //Check Valid Year
                 
                      DataTable Validate = queryYearPattern.CopyToDataTable();
                      queryYearPattern = Validate.AsEnumerable().Where(
                          r => !(YearPattern.IsMatch(r["YEAR"].ToString())));
              }

            if (queryTotalReg.Count() > 0)
            {
                Message += string.Format("<br>มีจำนวนผู้ลงทะเบียนได้จำนวน {0} รายการไม่อยู่ในรูปแบบตัวเลข กรุณาตรวจสอบและนำเข้าอีกครั้ง", queryTotalReg.Count());
                Valid = false;
            }
            if (queryRealreg.Count() > 0)
            {
                Message += string.Format("<br>มีจำนวนผู้ลงทะเบียนจริงจำนวน {0} รายการไม่อยู่ในรูปแบบตัวเลข กรุณาตรวจสอบและนำเข้าอีกครั้ง", queryRealreg.Count());
                Valid = false;
            }
            if (queryCourseCode.Count() > 0)
            {
                Message += string.Format("<br>มีรหัสวิชาจำนวน {0} รายการไม่อยู่ในรูปแบบตัวเลข กรุณาตรวจสอบและนำเข้าอีกครั้ง", queryCourseCode.Count());
                Valid = false;
            }
            if (querySection.Count() > 0)
            {
                Message += string.Format("<br>มีตอนเรียนได้จำนวน {0} รายการไม่อยู่ในรูปแบบตัวเลข กรุณาตรวจสอบและนำเข้าอีกครั้ง", querySection.Count());
                Valid = false;
            }
            if (queryFaccode.Count() > 0)
            {
                Message += string.Format("<br>มีรหัสคณะได้จำนวน {0} รายการไม่อยู่ในรูปแบบตัวเลข กรุณาตรวจสอบและนำเข้าอีกครั้ง", queryFaccode.Count());
                Valid = false;
            }
            if (queryYear.Count() > 0)
            {
                Message += string.Format("<br>มีเลขปีจำนวน {0} รายการไม่อยู่ในรูปแบบตัวเลข กรุณาตรวจสอบและนำเข้าอีกครั้ง", queryYear.Count());
                Valid = false;
            }
            if (queryYearPattern.Count() > 0)
            {
                Message += string.Format("<br>มีเลขปีจำนวน {0} รายการไม่อยู่ในรูปแบบปี 4 หลัก กรุณาตรวจสอบและนำเข้าอีกครั้ง", queryYearPattern.Count());
                Valid = false;
            }
            if (Valid)
            {
                return true;
            }
                else
            {
                return false;
            }
            }
        }
    }
