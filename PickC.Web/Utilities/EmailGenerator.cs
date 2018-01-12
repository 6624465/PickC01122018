using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace PickC.Web.Utilities
{
    public class EmailGenerator
    {
        Configuration config;
        public EmailGenerator()
        {
            config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        }

        public bool SendMail(MailMessage msg)
        {

            MailSettingsSectionGroup settings = (MailSettingsSectionGroup)config.GetSectionGroup("system.net/mailSettings");

            msg.From = new MailAddress(settings.Smtp.From, "PickC");
            SmtpClient client = new SmtpClient(settings.Smtp.Network.Host);
            client.Port = settings.Smtp.Network.Port;
            client.EnableSsl = settings.Smtp.Network.EnableSsl;
            client.Timeout = 900000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = settings.Smtp.Network.DefaultCredentials;
            client.Credentials = new NetworkCredential(settings.Smtp.Network.UserName, settings.Smtp.Network.Password);
            client.Send(msg);
            return true;
        }

        public bool ConfigMail(string to, string cc, string bcc, bool isHtml, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.CC.Add(new MailAddress(cc));
            msg.Bcc.Add(new MailAddress(bcc));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }

        public bool ConfigMail(string to, bool isHtml, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }

        public bool ConfigMail(string to, bool isHtml, string subject, string body)
        {
            MailMessage msg = new MailMessage();

            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);
        }

        public bool ConfigMail(string to, bool isHtml, string subject, string body, byte[] attBytes)
        {
            MailMessage msg = new MailMessage();

            msg.To.Add(new MailAddress(to));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            Attachment att = new Attachment(new MemoryStream(attBytes), "PickInvoice.Pdf");
            msg.Attachments.Add(att);

            return SendMail(msg);
        }

        public bool ConfigMail(string to, string bcc, bool isHtml, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));

            msg.Bcc.Add(new MailAddress(bcc));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }

        public bool ConfigMail(string to, bool isHtml, string cc, string subject, string body, string[] attachments)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(to));
            msg.CC.Add(new MailAddress(cc));

            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = UTF8Encoding.UTF8;
            msg.IsBodyHtml = isHtml;

            return SendMail(msg);

        }
    }
}