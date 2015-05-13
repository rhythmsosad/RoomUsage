using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RoomUsageApp.Classes
{
    public abstract class ValidationBase
    {
        public DataTable data { get; set; }
        public string Message { get; set; }

        public abstract bool IsValid();
    }
}