using System.Windows.Controls;
using Alsolos.Commons.Wpf.Mvvm;
using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Views
{
    public class EmailViewModel : ViewModel
    {
        public DelegateCommand<PasswordBox> SendEmailsCommand => BackingFields.GetCommand<PasswordBox>(SendEmails, CanSendEmails);

        public EmailPreviewViewModel EmailPreviewViewModel
        {
            get { return BackingFields.GetValue(() => new EmailPreviewViewModel()); }
        }

        public string SmtpUserName
        {
            get { return BackingFields.GetValue(() => SettingsService.Settings.SmtpUserName); }
            set { BackingFields.SetValue(value, userName => SettingsService.Settings.SmtpUserName = userName); }
        }

        private bool CanSendEmails(PasswordBox passwordBox)
        {
            passwordBox.Password = SettingsService.Settings.SmtpPassword;
            return true;
        }

        private void SendEmails(PasswordBox passwordBox)
        {
            var emailService = new EmailService(SmtpUserName, passwordBox.Password);
            emailService.SendEmail("hirsch@blaustein.ch", "Test", "Hallo Heiner\n\nDas iste ein Test. Funktioniert es?\n\nSchöne Grüsse\nHeiner");
        }
    }
}
