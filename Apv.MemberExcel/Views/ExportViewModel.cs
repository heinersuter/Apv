using Alsolos.Commons.Wpf.Mvvm;
using Apv.MemberExcel.Services;

namespace Apv.MemberExcel.Views
{
    public class ExportViewModel : ViewModel
    {
        private readonly LetterViewModel _letterViewModel;

        public ExportViewModel(LetterViewModel letterViewModel)
        {
            _letterViewModel = letterViewModel;
        }

        public DelegateCommand ExportEmailsCommand => BackingFields.GetCommand(ExportEmails, CanExportEmails);

        public DelegateCommand ExportAddressesCommand => BackingFields.GetCommand(ExportAddresses, CanExportAddresses);

        public DelegateCommand ExportVCardsCommand => BackingFields.GetCommand(ExportVCards, CanExportVCards);

        private bool CanExportEmails() => true;

        private void ExportEmails() => EmailExportService.CreateEmailExport(_letterViewModel.ExcelFilePath);

        private bool CanExportAddresses() => true;

        private void ExportAddresses() => AddressExportService.CreateAddressExport(_letterViewModel.ExcelFilePath);

        private bool CanExportVCards() => true;

        private void ExportVCards() => VCardExportService.CreateVCardFile(_letterViewModel.ExcelFilePath);
    }
}