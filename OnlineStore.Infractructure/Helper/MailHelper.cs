using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace OnlineStore.Infractructure.Helper
{
    public class MailHelper
    {
        public static void Send(string _mail, string title, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("mail.xxx");
                SmtpServer.UseDefaultCredentials = true;
                mail.From = new MailAddress("info@xxx");
                mail.To.Add(_mail);
                mail.Subject = title;
                mail.Body = body;
                mail.IsBodyHtml = true;
                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("info@xxx", "xxx");

                SmtpServer.Send(mail);
            }
            catch (Exception)
            {
            }
        }
    }
}