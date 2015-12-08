using System.Collections.Generic;

using Apv.Data.Model;

namespace Apv.Data.WindowsViewer.Service
{
    public class MemberService
    {
        public IEnumerable<Member> GetActiveMembers()
        {
            return ApvDataAccess.GetActiveMembers();
        }

        public void UpdateMember(Member member)
        {
            ApvDataAccess.UpdateMember(member);
        }
    }
}
