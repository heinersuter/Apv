using System.Collections.Generic;

using Apv.Data.Model;

namespace Apv.Data.WindowsViewer.Service
{
    public class MemberService
    {
        public IEnumerable<Member> GetMembers()
        {
            return ApvDataAccess.GetMembers();
        }

        public Member GetMemberDetails(Member member)
        {
            return ApvDataAccess.GetMemberDetails(member.Id);
        }

        public void UpdateMember(Member member)
        {
            ApvDataAccess.UpdateMember(member);
        }
    }
}
