using System;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace library_api.Models
{
    public class Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
        public SmtpClient Client;
        public MailMessage Message;

        public Email()
        {
            SmtpSettings();
        }

        private void SmtpSettings()
        {
            Client = new SmtpClient("smtp.mailtrap.io", 587)
            {
                Credentials = new NetworkCredential(
                    "efa18c0fef4efa",
                    "ad68d25154fe15"),
                EnableSsl = true
            };
        }

        public void SetMessage()
        {
            Message = new MailMessage(From, To)
            {
                Subject = Subject,
                Body = Body,
                IsBodyHtml = true
            };
        }

        public void SetBody(object model, string path)
        {
            string body = System.IO.File.ReadAllText(path);
            Type t = model.GetType();
            foreach (PropertyInfo prop in t.GetProperties())
            {
                var key = prop.Name;
                var value = prop.GetValue(model, null)?.ToString();
                body = body.Replace("{"+key+"}", value);
            }
            Body = body;
        }

        public object Send()
        {
            try
            {
                Client.Send(Message);
                return true;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
