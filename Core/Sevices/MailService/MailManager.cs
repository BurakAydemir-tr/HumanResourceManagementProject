using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Sevices.MailService
{
    public class MailManager : IMailService
    {
        readonly IConfiguration _configuration;

        public MailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendMail(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage();
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;
            foreach (var to in tos)
            {
                mail.To.Add(to);
            }
            mail.From = new(_configuration["MailSettings:Username"], "Kaptan Hrms", System.Text.Encoding.UTF8);

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(_configuration["MailSettings:Username"], _configuration["MailSettings:Password"]);
            smtp.Port =Convert.ToInt32(_configuration["MailSettings:Port"]);
            smtp.Host = _configuration["MailSettings:Host"];
            smtp.EnableSsl=true;
            smtp.Send(mail);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage();
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = isBodyHtml;
            //foreach (var to in tos)
            //{
            //    mail.To.Add(to);
            //}
            mail.To.Add("baydemir2035@gmail.com");
            mail.From = new(_configuration["MailSettings:Username"], "Kaptan Hrms", System.Text.Encoding.UTF8);

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(_configuration["MailSettings:Username"], _configuration["MailSettings:Password"]);
            smtp.Port = Convert.ToInt32(_configuration["MailSettings:Port"]);
            smtp.Host = _configuration["MailSettings:Host"];
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to, string userId, string resetToken)
        {
            StringBuilder mail = new StringBuilder();
            mail.AppendLine("Merhaba<br> Yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"http://localhost:4200");
            mail.AppendLine("/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">NOT: Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br>Kaptan HRMS");

            mail.AppendLine("<br><br>/update-password/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);

            await SendMailAsync(new string[] {to}, "Şifre Yenileme Talebi", mail.ToString());
        }
    }
}
