using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;

namespace RoomUsageApp.Core
{
    public class CoreFunction
    {
        public static string InnermostException(Exception exception)
        {
            string message = string.Empty;
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
            message = exception.Message;

            return message;
        }
    }
}