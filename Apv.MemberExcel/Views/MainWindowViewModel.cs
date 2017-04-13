using Alsolos.Commons.Wpf.Mvvm;

namespace Apv.MemberExcel.Views
{
    public class MainWindowViewModel : ViewModel
    {
        public LetterViewModel LetterViewModel => BackingFields.GetValue(() => new LetterViewModel());

        public EmailViewModel EmailViewModel => BackingFields.GetValue(() => new EmailViewModel(LetterViewModel));

        public GeocodeViewModel GeocodeViewModel => BackingFields.GetValue(() => new GeocodeViewModel());
    }
}
