using System.Collections.Generic;

using Apv.Data.Dtos;
using Apv.Data.Dtos.Members;

namespace Apv.Data.WindowsViewer.Service
{
    public interface IMemberService
    {
        IEnumerable<MemberDto> GetMembers();

        MemberDetailsDto GetMemberDetails(MemberDto member);

        void UpdateMember(MemberDetailsDto member);
    }
}