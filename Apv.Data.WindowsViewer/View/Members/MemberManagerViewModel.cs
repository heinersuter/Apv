using System.ComponentModel;

using Alsolos.Commons.Wpf.Mvvm;

using Apv.Data.WindowsViewer.Service;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class MemberManagerViewModel : ViewModel
    {
        private readonly IMemberService _memberService = new WebMemberService();

        public MemberManagerViewModel()
        {
            MemberList.PropertyChanged += MemberListOnPropertyChanged;
        }

        private void MemberListOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == GetPropertyName(() => MemberList.SelectedMember))
            {
                MemberDetails.SelectedMember = MemberList.SelectedMember != null ? _memberService.GetMemberDetails(MemberList.SelectedMember) : null;
            }
        }

        public MemberListViewModel MemberList
        {
            get { return BackingFields.GetValue(() => new MemberListViewModel(_memberService)); }
        }

        public MemberDetailsViewModel MemberDetails
        {
            get { return BackingFields.GetValue(() => new MemberDetailsViewModel(_memberService)); }
        }
    }
}
