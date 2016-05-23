using System;
using System.Linq;
using Alsolos.Commons.Wpf.Mvvm;
using Apv.MemberExcel.Pdfs;
using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Views
{
    public class LetterViewModel : ViewModel
    {
        public DelegateCommand CreatePdfsCommand => BackingFields.GetCommand(CreatePdfs, CanCreatePdfs);

        public string ExcelFilePath
        {
            get { return BackingFields.GetValue(() => SettingsService.Settings.AddressesExcelFile); }
            set { BackingFields.SetValue(value, path => SettingsService.Settings.AddressesExcelFile = path); }
        }

        public string PdfFolderPath
        {
            get { return BackingFields.GetValue(() => SettingsService.Settings.PdfOutputDirectory); }
            set { BackingFields.SetValue(value, path => SettingsService.Settings.PdfOutputDirectory = path); }
        }

        private bool CanCreatePdfs()
        {
            return true;
        }

        private void CreatePdfs()
        {
            var addressDtos = ExcelService.ReadAddresses(ExcelFilePath).Where(dto => dto.Status == Status.Active).ToArray();

            var families = addressDtos
                .Where(a => a.FamilyId != null)
                .GroupBy(a => a.FamilyId)
                .Select(LetterAddress.Combine);
            var singles = addressDtos.Where(a => a.FamilyId == null).Select(LetterAddress.Create);
            var addresses = families.Concat(singles).ToArray();

            var mailingAddresses = addresses.Where(dto => dto.AddressLine1 != null).ToArray();

            var pdfService = new PdfService(PdfFolderPath);
            var fullMailingAddresses = mailingAddresses.Where(dto => !dto.HasEmail).ToArray();
            pdfService.WriteFullMailingLetter(fullMailingAddresses, "Brief_Alles.pdf");

            var requiresDepositSlipAddresses = mailingAddresses.Where(dto => dto.HasEmail && dto.RequiresDepositSlip == true).ToArray();
            pdfService.WriteDepositSlipLetter(requiresDepositSlipAddresses, "Brief_Nur_EZ.pdf", false);

            var requiresDepositSlipUnknownAddresses = mailingAddresses.Where(dto => dto.HasEmail && dto.RequiresDepositSlip == null).ToArray();
            pdfService.WriteDepositSlipLetter(requiresDepositSlipUnknownAddresses, "Brief_EZ_Unbekannt.pdf", true);

            Console.WriteLine("Keine Post-Adresse: " + string.Join(", ", addressDtos.Where(dto => dto.AddressLine1 == null).Select(dto => $"{dto.Nickname}: {dto.Email1}")));

            System.Diagnostics.Process.Start(PdfFolderPath);
        }
    }
}
