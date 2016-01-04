using System.Collections.Generic;

using Apv.Data.Dtos;

namespace Apv.Data.WindowsViewer.Service
{
    public interface IMemberService
    {
        IEnumerable<MemberDto> GetMembers();

        MemberDetailsDto GetMemberDetails(MemberDto member);

        void UpdateMember(MemberDetailsDto member);
    }
}