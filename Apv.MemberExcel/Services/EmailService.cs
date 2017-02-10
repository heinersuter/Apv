using System.Net;
using System.Net.Mail;

namespace Apv.MemberExcel.Services
{
    public class EmailService
    {
        private readonly string _smtpUserName;
        private readonly string _smtpPassword;

        public EmailService(string smtpUserName, string smtpPassword)
        {
            _smtpUserName = smtpUserName;
            _smtpPassword = smtpPassword;
        }

        public void SendEmail(string to, string subject, string body)
        {
            using (var smtpClient = new SmtpClient
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = "mail.gmx.net",
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpUserName, _smtpPassword)
            })
            {
                using (var mailMessage = new MailMessage("heiner.suter@gmx.ch", to)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtpClient.Send(mailMessage);
                }
            }

        }
    }
}
