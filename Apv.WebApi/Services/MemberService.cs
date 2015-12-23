using System.Collections.Generic;

using Apv.Data;
using Apv.Data.Dtos;

namespace Apv.WebApi.Services
{
    public class MemberService
    {
        public IEnumerable<MemberDto> GetMembers()
        {
            return ApvDataAccess.GetMembers();
        }

        public MemberDetailsDto GetMemberDetails(long memberId)
        {
            return ApvDataAccess.GetMemberDetails(memberId);
        }

        public void UpdateMember(MemberDetailsDto member)
        {
            ApvDataAccess.UpdateMember(member);
        }
    }
}
