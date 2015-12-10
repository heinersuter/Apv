using Alsolos.Commons.Wpf.Mvvm;

using Apv.Data.Model;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class AddressesViewModel : ViewModel
    {
        public Member Member
        {
            get { return BackingFields.GetValue<Member>(); }
            set { BackingFields.SetValue(value); }
        }
    }
}
