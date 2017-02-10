using Alsolos.Commons.Wpf.Mvvm;

namespace Apv.MemberExcel.Views
{
    public class MainWindowViewModel : ViewModel
    {
        public LetterViewModel LetterViewModel
        {
            get { return BackingFields.GetValue(() => new LetterViewModel()); }
        }

        public EmailViewModel EmailViewModel
        {
            get { return BackingFields.GetValue(() => new EmailViewModel(LetterViewModel)); }
        }
    }
}
