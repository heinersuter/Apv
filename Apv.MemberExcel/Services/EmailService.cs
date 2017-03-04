using System.Net;
using System.Net.Mail;
using System.Text;
using Apv.MemberExcel.Email;

namespace Apv.MemberExcel.Services
{
    public class EmailService
    {
        private readonly string _smtpHost;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public EmailService(string smtpHost, string smtpUsername, string smtpPassword)
        {
            _smtpHost = smtpHost;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }

        public void SendEmail(EmailDto email)
        {
            using (var smtpClient = new SmtpClient
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = _smtpHost,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpUsername, _smtpPassword),
                DeliveryFormat = SmtpDeliveryFormat.International
            })
            {
                using (var mailMessage = new MailMessage("heiner.suter@gmx.ch", email.To)
                {
                    Subject = email.Subject,
                    Body = email.Body,
                    BodyEncoding = Encoding.UTF8,
                })
                {
                    foreach (var attachement in email.Attachements)
                    {
                        mailMessage.Attachments.Add(new Attachment(attachement));
                    }
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
