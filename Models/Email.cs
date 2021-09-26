using System;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OMS.Models
{
    public class Email
    {
        public string email { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        Connection con = new Connection();
        public void SendEmail(Email e)
        {                        
            SmtpClient clientDetails = new SmtpClient();
            clientDetails.Port = 587;
            clientDetails.Host = "smtp.gmail.com"; //"smtp-mail.outlook.com";
            clientDetails.EnableSsl = true;
            clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;
            clientDetails.UseDefaultCredentials = false;
            clientDetails.Credentials = new NetworkCredential("ansarmuahammadfaraz@gmail.com", "0123456789faraz@ned@faraz");
            MailMessage mailDetails = new MailMessage();
            mailDetails.From = new MailAddress("ansarmuahammadfaraz@gmail.com");
            mailDetails.To.Add(e.email);
            mailDetails.Subject = e.Subject;
            mailDetails.Body = e.Body;
            clientDetails.Send(mailDetails);
        }
    }
}