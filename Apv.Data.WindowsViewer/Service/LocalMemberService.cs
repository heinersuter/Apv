using System.Collections.Generic;

using Apv.Data.Dtos;

namespace Apv.Data.WindowsViewer.Service
{
    public class LocalMemberService : IMemberService
    {
        public IEnumerable<MemberDto> GetMembers()
        {
            return ApvDataAccess.GetMembers();
        }

        public MemberDetailsDto GetMemberDetails(MemberDto member)
        {
            return ApvDataAccess.GetMemberDetails(member.Id);
        }

        public void UpdateMember(MemberDetailsDto member)
        {
            ApvDataAccess.UpdateMember(member);
        }
    }
}
