using Alsolos.Commons.Wpf.Mvvm;

using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Views
{
    public class MainViewModel : ViewModel
    {
        public DelegateCommand ReadExcelCommand => BackingFields.GetCommand(ReadExcel, CanReadExcel);

        public string ExcelFilePath
        {
            get { return BackingFields.GetValue<string>(); }
            set { BackingFields.SetValue(value); }
        }

        public string PdfFilePath
        {
            get { return BackingFields.GetValue<string>(); }
            set { BackingFields.SetValue(value); }
        }

        private bool CanReadExcel()
        {
            return true;
        }

        private void ReadExcel()
        {
            var addresses = ExcelService.ReadAddresses(@"C:\Users\hsu\Desktop\Addresses.xlsx");
            PdfService.WritePdf(addresses, @"C:\Users\hsu\Desktop\Addresses.pdf");
            System.Diagnostics.Process.Start(@"C:\Users\hsu\Desktop\Addresses.pdf");
        }
    }
}
