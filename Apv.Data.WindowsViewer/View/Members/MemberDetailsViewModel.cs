using Alsolos.Commons.Wpf.Mvvm;

using Apv.Data.Dtos;
using Apv.Data.Dtos.Members;
using Apv.Data.WindowsViewer.Service;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class MemberDetailsViewModel : ViewModel
    {
        private readonly IMemberService _memberService;

        public MemberDetailsViewModel(IMemberService memberService)
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

        public PhoneNumbersViewModel PhoneNumbersViewModel
        {
            get { return BackingFields.GetValue(() => new PhoneNumbersViewModel()); }
        }

        public EmailAddressesViewModel EmailAddressesViewModel
        {
            get { return BackingFields.GetValue(() => new EmailAddressesViewModel()); }
        }

        public NotesViewModel NotesViewModel
        {
            get { return BackingFields.GetValue(() => new NotesViewModel()); }
        }

        public FunctionsViewModel FunctionsViewModel
        {
            get { return BackingFields.GetValue(() => new FunctionsViewModel()); }
        }

        public CommunicationViewModel CommunicationViewModel
        {
            get { return BackingFields.GetValue(() => new CommunicationViewModel()); }
        }

        public MemberDetailsDto SelectedMember
        {
            get { return BackingFields.GetValue<MemberDetailsDto>(); }
            set { BackingFields.SetValue(value, UpdateMember); }
        }

        private void UpdateMember(MemberDetailsDto member)
        {
            BaseMemberDataViewModel.Member = member;
            AddressesViewModel.Member = member;
            PhoneNumbersViewModel.Member = member;
            EmailAddressesViewModel.Member = member;
            NotesViewModel.Member = member;
            FunctionsViewModel.Member = member;
            CommunicationViewModel.Member = member;
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
