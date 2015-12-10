using Alsolos.Commons.Wpf.Mvvm;

using Apv.Data.Model;
using Apv.Data.WindowsViewer.Service;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class MemberDetailsViewModel : ViewModel
    {
        private readonly MemberService _memberService;

        public MemberDetailsViewModel(MemberService memberService)
        {
            _memberService = memberService;
        }

        public BaseMemberDataViewModel BaseMemberDataViewModel
        {
            get { return BackingFields.GetValue(() => new BaseMemberDataViewModel()); }
        }

        public AddressesViewModel AddressesViewModel
        {
            get { return BackingFields.GetValue(() => new AddressesViewModel()); }
        }

        public Member SelectedMember
        {
            get { return BackingFields.GetValue<Member>(); }
            set { BackingFields.SetValue(value, UpdateMember); }
        }

        private void UpdateMember(Member member)
        {
            BaseMemberDataViewModel.Member = member;
            AddressesViewModel.Member = member;
        }

        public DelegateCommand SaveCommand => BackingFields.GetCommand(Save, CanSave);

        private bool CanSave()
        {
            return SelectedMember != null;
        }

        private void Save()
        {
            _memberService.UpdateMember(SelectedMember);
        }
    }
}
