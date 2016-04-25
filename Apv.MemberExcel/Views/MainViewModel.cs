using System;
using System.Linq;
using Alsolos.Commons.Wpf.Mvvm;

using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Views
{
    public class MainViewModel : ViewModel
    {
        public DelegateCommand CreatePdfsCommand => BackingFields.GetCommand(CreatePdfs, CanCreatePdfs);

        public string ExcelFilePath
        {
            get { return BackingFields.GetValue<string>(() => @"C:\Users\heine\OneDrive\APV\Adressen.xlsx"); }
            set { BackingFields.SetValue(value); }
        }

        public string PdfFolderPath
        {
            get { return BackingFields.GetValue<string>(() => @"C:\Users\heine\OneDrive\APV"); }
            set { BackingFields.SetValue(value); }
        }

        public bool RequiresDepositSlip
        {
            get { return BackingFields.GetValue<bool>(() => true); }
            set { BackingFields.SetValue(value); }
        }

        public bool RequiresMailing
        {
            get { return BackingFields.GetValue<bool>(() => true); }
            set { BackingFields.SetValue(value); }
        }

        private bool CanCreatePdfs()
        {
            return true;
        }

        private void CreatePdfs()
        {
            var pdfService = new PdfService(PdfFolderPath);
            var addresses = ExcelService.ReadAddresses(ExcelFilePath).Where(dto => dto.Status == Status.Active).ToArray();
            var mailingAddresses = addresses.Where(dto => dto.AddressLine1 != null).ToArray();

            var fullMailingAddresses = mailingAddresses.Where(dto => dto.Email1 == null).ToArray();
            pdfService.WriteFullMailingLetter(fullMailingAddresses, "Brief_Alles.pdf");

            var requiresDepositSlipAddresses = mailingAddresses.Where(dto => dto.Email1 != null && dto.RequiresDepositSlip == true).ToArray();
            pdfService.WriteDepositSlipLetter(requiresDepositSlipAddresses, "Brief_Nur_EZ.pdf", false);

            var requiresDepositSlipUnknownAddresses = mailingAddresses.Where(dto => dto.Email1 != null && dto.RequiresDepositSlip == null).ToArray();
            pdfService.WriteDepositSlipLetter(requiresDepositSlipUnknownAddresses, "Brief_EZ_Unbekannt.pdf", true);

            Console.WriteLine(string.Join(", ", addresses.Where(dto => dto.AddressLine1 == null).Select(dto => $"{dto.Nickname}: {dto.Email1}")));

            System.Diagnostics.Process.Start(PdfFolderPath);
        }
    }
}
