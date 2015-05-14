using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text.RegularExpressions; 

namespace RoomUsageApp.Classes
{
    public class CheckFormatTime : ValidationBase
    {
        // กำลัง ศึกษา linq อยุ่นะ

        bool checkPattern(string ColName)
        {
            //1. Check other data not in AR,IR
            var query = data.AsEnumerable().Where(
            r => !(r[ColName].ToString() == "AR" || r[ColName].ToString() == "IA")
            );
            // เจอแต่ AR กับ IA
            if(query.Count() == 0)
                return true;
            // มีอย่างอื่นด้วย
            DataTable tmpData = query.CopyToDataTable();
            int nRow = tmpData.AsEnumerable().Count();

            //2. Check data len > 4
            query = tmpData.AsEnumerable().Where(
             o => !(o[ColName].ToString().Length > 4 || o[ColName].ToString().Length < 3)
              );
           
            if (nRow != query.Count())
            {
                Message = string.Format("มีแถวที่ความยาวของ {0} ไม่อยู่ในช่วง 3-4 หลัก จำนวน {1} แถว กรุณาตรวจสอบและนำเข้าอีกครั้ง", ColName, nRow);
                return false;
            }
            else
            {
                tmpData = query.CopyToDataTable();
            }
            

            //3  Check time pattern for 3 digit
            query = tmpData.AsEnumerable().Where(
                o => o[ColName].ToString().Length == 3 );

            if (query.Count() > 0)
            {
                DataTable dt3digit = query.CopyToDataTable();
                Regex timePattern = new Regex(@"[1-9][0-5][0-9]$");
                query = dt3digit.AsEnumerable().Where(
                    r => !(timePattern.IsMatch(r[ColName].ToString())));
                if (query.Count() > 0)
                {
                    Message = string.Format("มีแถวที่ ข้อมูลของ {0} ไม่อยู่ในรูปแบบเวลา จำนวน {1} แถว กรุณาตรวจสอบและนำเข้าอีกครั้ง", ColName, query.Count());
                    return false;
                }
            }

            //4  Check time pattern for 4 digit
            query = tmpData.AsEnumerable().Where(
                o => o[ColName].ToString().Length == 4 );

            if (query.Count() > 0)
            {
                DataTable dt4digit = query.CopyToDataTable();
                Regex timePattern = new Regex(@"^(?:[01][0-9]|2[0-3])[0-5][0-9]$");

                query = dt4digit.AsEnumerable().Where(
                    r => !(timePattern.IsMatch(r[ColName].ToString())));
               
                if (query.Count() > 0)
                {
                    Message = string.Format("มีแถวที่ ข้อมูลของ {0} ไม่อยู่ในรูปแบบเวลา จำนวน {1} แถว กรุณาตรวจสอบและนำเข้าอีกครั้ง", ColName, query.Count());
                    return false;
                }
            }
            return true;
        }

      
        public override bool IsValid()
        {
           
            
            if (!checkPattern("STARTTIME"))
                return false;
  
            if (!checkPattern("ENDTIME"))
                return false;

            return true;
        }
    }
}