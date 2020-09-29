using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IdentityAPIWithoutEF.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var from = _config.GetValue<string>("Email:Email");
                var password = _config.GetValue<string>("Email:Password");

                var message = new MailMessage
                {
                    From = new MailAddress(from),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                message.To.Add(new MailAddress(to));

                var smtp = new SmtpClient
                {
                    Port = _config.GetValue<int>("Email:Port"),
                    Host = _config.GetValue<string>("Email:Host"),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(from, password),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                await smtp.SendMailAsync(message);
            }
            catch (Exception) { }
        }
    }
}
