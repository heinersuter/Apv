using System;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using Alsolos.Commons.Wpf.Mvvm;
using Apv.MemberExcel.Email;
using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Views
{
    public class EmailViewModel : ViewModel
    {
        private readonly LetterViewModel _letterViewModel;
        private EmailDto[] _emailDtos;

        public EmailViewModel(LetterViewModel letterViewModel)
        {
            _letterViewModel = letterViewModel;
        }

        public DelegateCommand GenerateEmailsCommand => BackingFields.GetCommand(GenerateEmails);

        public DelegateCommand<PasswordBox> SendTestEmailCommand => BackingFields.GetCommand<PasswordBox>(SendTestEmail, CanSendTestEmail);

        public DelegateCommand<PasswordBox> SendEmailsCommand => BackingFields.GetCommand<PasswordBox>(SendEmails);

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

        public string TestEmailAddress
        {
            get { return BackingFields.GetValue(() => SettingsService.Settings.TestEmailAddress); }
            set { BackingFields.SetValue(value, address => SettingsService.Settings.TestEmailAddress = address); }
        }

        private bool CanSendTestEmail(PasswordBox passwordBox)
        {
            passwordBox.Password = SettingsService.Settings.SmtpPassword;
            return _emailDtos != null;
        }

        private void SendTestEmail(PasswordBox passwordBox)
        {
            var emailDto = _emailDtos[EmailPreviewViewModel.Index];
            emailDto.To = TestEmailAddress;

            var emailService = new EmailService(SmtpHost, SmtpUsername, passwordBox.Password);
            emailService.SendEmail(emailDto);
        }

        private void GenerateEmails()
        {
            var addressDtos = ExcelService.ReadAddresses(_letterViewModel.ExcelFilePath)
                .Where(dto => dto.Status == Status.Active && dto.Email1 != null).ToArray();
            _emailDtos = EmailWriter.CreateEmails(addressDtos).ToArray();
            EmailPreviewViewModel.SetEmails(_emailDtos);
        }

        private void SendEmails(PasswordBox passwordBox)
        {
            Console.WriteLine($"Going to send {_emailDtos.Count()} emails");

            var emailService = new EmailService(SmtpHost, SmtpUsername, passwordBox.Password);
            foreach (var emailDto in _emailDtos)
            {
                emailService.SendEmail(emailDto);
                Console.WriteLine($"Email sent to {emailDto.To}");
                Thread.Sleep(TimeSpan.FromSeconds(20));
            }
        }
    }
}
