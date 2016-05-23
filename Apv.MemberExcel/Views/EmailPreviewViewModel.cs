using Alsolos.Commons.Wpf.Mvvm;

namespace Apv.MemberExcel.Views
{
    public class EmailPreviewViewModel : ViewModel
    {
        public string To
        {
            get { return BackingFields.GetValue<string>(); }
            set { BackingFields.SetValue(value); }
        }

        public string Subject
        {
            get { return BackingFields.GetValue<string>(); }
            set { BackingFields.SetValue(value); }
        }

        public string Body
        {
            get { return BackingFields.GetValue<string>(); }
            set { BackingFields.SetValue(value); }
        }
    }
}
