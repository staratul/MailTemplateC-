using EmailTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.IO;

namespace EmailTemplate.Services
{
    public class MailService
    {
        public void SendContactUsEmail(dynamic formData)
        {
            // Load the email template from file
            string templatePath = System.Web.Hosting.HostingEnvironment.MapPath("~/Templates/EmailTemplate.html");
            string emailBody = File.ReadAllText(templatePath);

            // Replace placeholders with form data
            emailBody = emailBody.Replace("{{Name}}", formData.Name);
            emailBody = emailBody.Replace("{{Email}}", formData.Email);
            emailBody = emailBody.Replace("{{Subject}}", formData.Subject);
            emailBody = emailBody.Replace("{{Message}}", formData.Message);

            // Email subject
            string subject = "Contact Us Form Submission - Planet Travel";

            // SMTP client using settings from web.config
            SmtpClient smtp = new SmtpClient
            {
                Host = WebConfigurationManager.AppSettings["SmtpHost"],
                Port = Convert.ToInt32(WebConfigurationManager.AppSettings["SmtpPort"]),
                EnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["EnableSsl"]),
                Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["SmtpUsername"], WebConfigurationManager.AppSettings["SmtpPassword"])
            };

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(WebConfigurationManager.AppSettings["FromEmail"]),
                Subject = subject,
                Body = emailBody,
                IsBodyHtml = true
            };
            mail.To.Add(WebConfigurationManager.AppSettings["AdminEmail"]);

            smtp.Send(mail);
        }
    }
}