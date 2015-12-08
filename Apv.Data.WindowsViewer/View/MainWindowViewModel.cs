using Alsolos.Commons.Wpf.Mvvm;
using Apv.Data.WindowsViewer.View.Members;

namespace Apv.Data.WindowsViewer.View
{
    public class MainWindowViewModel : ViewModel
    {
        public MemberManagerViewModel MemberManager => BackingFields.GetValue(() => new MemberManagerViewModel());
    }
}
