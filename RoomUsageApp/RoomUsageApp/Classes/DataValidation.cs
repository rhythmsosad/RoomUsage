using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace RoomUsageApp.Classes
{
    public class DataValidation
    {
        private DataTable data = new DataTable();
        public List<string> ErrorMessage = new List<string>();

        /// <summary>
        /// Data Validator
        /// </summary>
        /// <param name="inputData">DataTable to be validated</param>
        public DataValidation(DataTable inputData)
        {
            data = inputData;
        }

        public bool IsValid()
        {

            bool result = true;
            ErrorMessage = new List<string>();

            // Add Validaton แต่ละตัวลงไปใน List นี้
            List<ValidationBase> rules = new List<ValidationBase>()
            {
                new DayValidation() { data = data },
                new NumberValidaton() { data = data }
                //,
                //new CheckFormatTime() { data = data }
            };


            foreach (ValidationBase rule in rules)
            {
                if (!rule.IsValid())
                {
                    result = false;
                    ErrorMessage.Add(rule.Message);
                }
            }



            return result;
        }
    }
}