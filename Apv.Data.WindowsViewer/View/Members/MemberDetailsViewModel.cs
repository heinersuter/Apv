using System;
using System.Collections.Generic;
using System.Linq;

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

        public AddressesViewModel AddressesViewModel
        {
            get { return BackingFields.GetValue(() => new AddressesViewModel()); }
        }

        public Member SelectedMember
        {
            get { return BackingFields.GetValue<Member>(); }
            set { BackingFields.SetValue(value, member => AddressesViewModel.Member = member); }
        }

        public DelegateCommand SaveCommand => BackingFields.GetCommand(Save, CanSave);

        public IEnumerable<MemberStatus> Statuses => Enum.GetValues(typeof(MemberStatus)).Cast<MemberStatus>();

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
