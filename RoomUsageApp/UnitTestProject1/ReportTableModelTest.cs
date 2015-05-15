using Microsoft.VisualStudio.TestTools.UnitTesting;
using RoomUsageApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace UnitTestProject1
{
    [TestClass]
    class ReportTableModelTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ReportTableModel model = new ReportTableModel();
            model.BuildingNo = "320005";
            DataTable dt = model.QueryReport();
        }
    }
}
