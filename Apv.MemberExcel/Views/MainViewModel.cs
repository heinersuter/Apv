using Alsolos.Commons.Wpf.Mvvm;

using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Views
{
    public class MainViewModel : ViewModel
    {
        private readonly ExcelService _excelService = new ExcelService();
        private readonly PdfService _pdfService = new PdfService();

        public DelegateCommand ReadExcelCommand
        {
            get { return BackingFields.GetCommand(ReadExcel, CanReadExcel); }
        }

        private bool CanReadExcel()
        {
            return true;
        }

        private void ReadExcel()
        {
            var addresses = _excelService.ReadAddresses(@"C:\Users\hsu\Desktop\Addresses.xlsx");
            _pdfService.WritePdf(addresses, "addresses.pdf");
            System.Diagnostics.Process.Start("addresses.pdf");
        }
    }
}
