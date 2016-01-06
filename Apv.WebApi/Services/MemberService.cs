using System.Collections.Generic;

using Apv.Data;
using Apv.Data.Dtos;

namespace Apv.WebApi.Services
{
    public class MemberService
    {
        private readonly ApvDataAccess _apvDataAccess;

        public MemberService()
        {
            _apvDataAccess = new ApvDataAccess("Data Source=.;Initial Catalog=apv;Integrated Security=True");
        }

        public IEnumerable<MemberDto> GetMembers()
        {
            return _apvDataAccess.GetMembers();
        }

        public MemberDetailsDto GetMemberDetails(long memberId)
        {
            return _apvDataAccess.GetMemberDetails(memberId);
        }

        public void UpdateMember(MemberDetailsDto member)
        {
            _apvDataAccess.UpdateMember(member);
        }
    }
}
