using Excel;
using RoomUsageApp.Classes;
using RoomUsageApp.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

                        using (var entities = new DB_CHINEntities())
                        {
                            using (var transaction = entities.Database.BeginTransaction())
                            {
                                try
                                {
                                    TypeConverter convDouble = TypeDescriptor.GetConverter(typeof(double?));

                                    entities.RegistrationTemp.AddRange(result.Tables[0].AsEnumerable().Select(o => new RegistrationTemp() {
                                        BUILDINGNAME = o["BUILDINGNAME"].ToString(),
                                        BUILDINGNO = (double?)convDouble.ConvertFrom(o["BUILDINGNO"].ToString()),
                                        COURSECODE = (double?)convDouble.ConvertFrom(o["COURSECODE"].ToString()),
                                        DAY1 = o["DAY1"].ToString(),
                                        DAY2 = o["DAY2"].ToString(),
                                        DAY3 = o["DAY3"].ToString(),
                                        DAY4 = o["DAY4"].ToString(),
                                        DAY5 = o["DAY5"].ToString(),
                                        DAY6 = o["DAY6"].ToString(),
                                        DAY7 = o["DAY7"].ToString(),
                                        ENDTIME = (double?)convDouble.ConvertFrom(o["ENDTIME"].ToString()),
                                        FACABBR = o["FACABBR"].ToString(),
                                        FACCODE = (double?)convDouble.ConvertFrom(o["FACCODE"].ToString()),
                                        FACNAME = o["FACNAME"].ToString(),
                                        NAMEENGABBR = o["NAMEENGABBR"].ToString(),
                                        NUMCLASSSEAT = (double?)convDouble.ConvertFrom(o["NUMCLASSSEAT"].ToString()),
                                        REALREG = (double?)convDouble.ConvertFrom(o["REALREG"].ToString()),
                                        ROOMNO = (double?)convDouble.ConvertFrom(o["ROOMNO"].ToString()),
                                        ROOMTYPE = o["ROOMTYPE"].ToString(),
                                        SECTION = (double?)convDouble.ConvertFrom(o["SECTION"].ToString()),
                                        SEMESTER = (double?)convDouble.ConvertFrom(o["SEMESTER"].ToString()),
                                        STARTTIME = (double?)convDouble.ConvertFrom(o["STARTTIME"].ToString()),
                                        STUDYPROGRAM = o["STUDYPROGRAM"].ToString(),
                                        TEACHTYPE = o["TEACHTYPE"].ToString(),
                                        TOTALREG = (double?)convDouble.ConvertFrom(o["TOTALREG"].ToString()),
                                        YEAR = (double?)convDouble.ConvertFrom(o["YEAR"].ToString())
                                    }));

                                    entities.SaveChanges();

                                    transaction.Commit();
                                }
                                catch (Exception ex)
                                {
                                    transaction.Rollback();
                                }
                                finally
                                {

                                }
                            }
                        }

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