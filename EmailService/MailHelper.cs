using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailService
{
    public class MailHelper
    {
        public void SendEmail(string toEmail, string subject, string content)
        {
            var fromEmail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
            var mailDisplay = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
            var password = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
            var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
            var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
            bool enableSSL = bool.Parse(ConfigurationManager.AppSettings["EnableSSL"].ToString());

            MailMessage message = new MailMessage(new MailAddress(fromEmail, mailDisplay), new MailAddress(toEmail));
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = content;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmail, password);
            client.Host = smtpHost;
            client.EnableSsl = enableSSL;
            client.Port = !string.IsNullOrEmpty(smtpPort) ? Convert.ToInt32(smtpPort) : 0;
            client.Send(message);
        }
    }
}
