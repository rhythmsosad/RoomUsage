using Excel;
using RoomUsageApp.Classes;
using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RoomUsageApp.Controllers
{
    public class ImportController : Controller
    {
        // GET: Import
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FileUploadHandler()
        {
            foreach (var fileKey in Request.Files.AllKeys)
            {
                var file = Request.Files[fileKey];
                try
                {
                    if (file != null)
                    {
                        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(file.InputStream);

                        excelReader.IsFirstRowAsColumnNames = true;
                        DataSet result = excelReader.AsDataSet();

                        #region commented: display data
                        // Format to display row data
                        //StringBuilder sbRowFormat = new StringBuilder();
                        //int columnCount = 0;
                        //string rowFormat = string.Empty;

                        //StringBuilder sb = new StringBuilder();
                        //sb.Append("<table class=\"table table-striped table-bordered table-hover dataTables-example\">");
                        //sb.Append("<thead>");
                        //sb.Append("<tr>");

                        //sbRowFormat.Append("<tr>");
                        //foreach(DataColumn column in result.Tables[0].Columns)
                        //{
                        //    sb.AppendFormat("<th>{0}</th>", column.ColumnName);
                        //    sb.Append("");

                        //    sbRowFormat.AppendFormat("<td>{{{0}}}</td>", columnCount++);
                        //}
                        //sbRowFormat.Append("</tr>");
                        //rowFormat = sbRowFormat.ToString();

                        //sb.Append("</tr>");
                        //sb.Append("<tbody>");
                        //foreach(DataRow row in result.Tables[0].AsEnumerable().Take(100))
                        //{
                        //    sb.AppendFormat(rowFormat, row.ItemArray);
                        //}
                        //sb.Append("</tbody>");
                        //sb.Append("</thead>");
                        //sb.Append("</table>");
                        #endregion

                        DataValidation validator = new DataValidation(result.Tables[0]);
                        bool validateResult = validator.IsValid();

                        //using (var entities = new DB_CHINEntities())
                        //{
                        //    using (var transaction = entities.Database.BeginTransaction())
                        //    {
                        //        entities.Schedule
                        //    }
                        //}

                        excelReader.Close();

                        //string html = sb.ToString();
                        if (validateResult)
                        {
                            return Json(new { Message = "Import Success" });
                        }
                        else
                        {
                            return Json(new { Message = string.Format("Data is not valid : {0}", string.Join("<br />", validator.ErrorMessage.ToArray())) });
                        }
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { Message = "Error in saving file" });
                }
            }
            return Json(new { Message = "Error in saving file" });
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}