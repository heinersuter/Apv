using System.Linq;
using Alsolos.Commons.Wpf.Mvvm;

using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Views
{
    public class MainViewModel : ViewModel
    {
        public DelegateCommand ReadExcelCommand => BackingFields.GetCommand(ReadExcel, CanReadExcel);

        public string ExcelFilePath
        {
            get { return BackingFields.GetValue<string>(() => @"C:\Users\heine\OneDrive\APV\Adressen.xlsx"); }
            set { BackingFields.SetValue(value); }
        }

        public string PdfFilePath
        {
            get { return BackingFields.GetValue<string>(() => @"C:\Users\heine\OneDrive\APV\Adressen.pdf"); }
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

        private bool CanReadExcel()
        {
            return true;
        }

        private void ReadExcel()
        {
            var addresses = ExcelService.ReadAddresses(ExcelFilePath);
            PdfService.WritePdf(addresses.Where(dto => !dto.RequiresMailing != RequiresMailing && !dto.RequiresDepositSlip != RequiresDepositSlip), PdfFilePath);
            System.Diagnostics.Process.Start(PdfFilePath);
        }
    }
}
