using System.Linq;
using System.Windows.Controls;
using Alsolos.Commons.Wpf.Mvvm;
using Apv.MemberExcel.Email;
using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Views
{
    public class EmailViewModel : ViewModel
    {
        public EmailViewModel(LetterViewModel letterViewModel)
        {
            var addressDtos = ExcelService.ReadAddresses(letterViewModel.ExcelFilePath)
                .Where(dto => dto.Status == Status.Active && dto.Email1 != null).ToArray();
            var emailDtos = EmailWriter.CreateEmails(addressDtos);
            EmailPreviewViewModel.SetEmails(emailDtos);
        }

        public DelegateCommand<PasswordBox> SendEmailsCommand => BackingFields.GetCommand<PasswordBox>(SendEmails, CanSendEmails);

        public EmailPreviewViewModel EmailPreviewViewModel
        {
            get { return BackingFields.GetValue(() => new EmailPreviewViewModel()); }
        }

        public string SmtpHost
        {
            get { return BackingFields.GetValue(() => SettingsService.Settings.SmtpHost); }
            set { BackingFields.SetValue(value, host => SettingsService.Settings.SmtpHost = host); }
        }

        public string SmtpUsername
        {
            get { return BackingFields.GetValue(() => SettingsService.Settings.SmtpUsername); }
            set { BackingFields.SetValue(value, userName => SettingsService.Settings.SmtpUsername = userName); }
        }

        private bool CanSendEmails(PasswordBox passwordBox)
        {
            passwordBox.Password = SettingsService.Settings.SmtpPassword;
            return true;
        }

        private void SendEmails(PasswordBox passwordBox)
        {
            var emailService = new EmailService(SmtpHost, SmtpUsername, passwordBox.Password);
            emailService.SendEmail(new EmailDto
            {
                To = "hirsch@blaustein.ch",
                Subject = "APV - Test",
                Attachements = { @"Z:\APV\pdfs\GV_Protokoll_2016.pdf" },
                Body = "Hallo Heiner\n\nDas iste ein Test. Funktioniert es?\n\nSchöne Grüsse\nHeiner"
            });
        }
    }
}
