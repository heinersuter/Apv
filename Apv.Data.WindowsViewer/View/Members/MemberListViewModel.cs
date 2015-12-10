using System.Collections.Generic;

using Alsolos.Commons.Wpf.Mvvm;

using Apv.Data.Model;
using Apv.Data.WindowsViewer.Service;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class MemberListViewModel : ViewModel
    {
        private readonly MemberService _memberService;

        public MemberListViewModel(MemberService memberService)
        {
            _memberService = memberService;

            Load();
        }

        public IEnumerable<Member> Members
        {
            get { return BackingFields.GetValue<IEnumerable<Member>>(); }
            set { BackingFields.SetValue(value); }
        }

        public Member SelectedMember
        {
            get { return BackingFields.GetValue<Member>(); }
            set { BackingFields.SetValue(value); }
        }

        public void Load()
        {
            Members = _memberService.GetMembers();
        }
    }
}
