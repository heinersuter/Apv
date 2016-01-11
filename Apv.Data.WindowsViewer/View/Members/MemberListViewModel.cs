using System.Collections.Generic;
using System.Windows.Data;

using Alsolos.Commons.Wpf.Mvvm;

using Apv.Data.Dtos;
using Apv.Data.Dtos.Members;
using Apv.Data.WindowsViewer.Extensions;
using Apv.Data.WindowsViewer.Service;

namespace Apv.Data.WindowsViewer.View.Members
{
    public class MemberListViewModel : ViewModel
    {
        private readonly IMemberService _memberService;

        public MemberListViewModel(IMemberService memberService)
        {
            _memberService = memberService;

            Load();
        }

        public string Filter
        {
            get { return BackingFields.GetValue<string>(); }
            set { BackingFields.SetValue(value, ApplyFilter); }
        }

        private void ApplyFilter(string filter)
        {
            var collectionView = CollectionViewSource.GetDefaultView(Members);
            collectionView.Filter = sender =>
            {
                if (string.IsNullOrWhiteSpace(filter))
                {
                    return true;
                }
                var member = sender as MemberDto;
                if (member == null)
                {
                    return false;
                }
                if (member.Nickname.ContainsIgnoreCase(filter) || member.Firstname.ContainsIgnoreCase(filter) || member.Lastname.ContainsIgnoreCase(filter))
                {
                    return true;
                }
                return false;
            };
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

        private void Load()
        {
            Members = _memberService.GetMembers();
        }
    }
}
