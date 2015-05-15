using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data; // require อันนี้เสมอ เพราะจะใช้ .AsEnumurable

namespace RoomUsageApp.Classes
{
    public class teeValidation : ValidationBase
    {
        // สมมติคลาสนี้จะเช็คว่า Course Code ห้ามเกิน 6 หลัก (สมมตินะ)
        public override bool IsValid()
        {
            int invalidRow = data.AsEnumerable().Where(o => o["COURSECODE"].ToString().Length > 6).Count();
            if(invalidRow > 0)
            {
                Message = string.Format("มีแถวที่ Course Code เกินกว่า 6 หลักจำนวน {0} แถว กรุณาตรวจสอบและนำเข้าอีกครั้ง", invalidRow);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}