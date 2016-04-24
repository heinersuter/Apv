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
            var addresses = ExcelService.ReadAddresses(ExcelFilePath).ToArray();

            var fullMailingAddresses = addresses.Where(dto => dto.Email1 == null).ToArray();
            pdfService.WriteEnvelopes(fullMailingAddresses, "FullMailing_Envelopes.pdf");
            pdfService.WriteFullMailingLetter(fullMailingAddresses, "FullMailing_Letters.pdf");

            System.Diagnostics.Process.Start(PdfFolderPath);
        }
    }
}
