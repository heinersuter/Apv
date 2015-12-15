using System.Collections.Generic;

using Alsolos.Commons.Wpf.Mvvm;

using Apv.Data.Dtos;
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

        public IEnumerable<MemberDto> Members
        {
            get { return BackingFields.GetValue<IEnumerable<MemberDto>>(); }
            set { BackingFields.SetValue(value); }
        }

        public MemberDto SelectedMember
        {
            get { return BackingFields.GetValue<MemberDto>(); }
            set { BackingFields.SetValue(value); }
        }

        public void Load()
        {
            Members = _memberService.GetMembers();
        }
    }
}
