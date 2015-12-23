using System.Collections.Generic;

using Apv.Data.Dtos;
using Apv.WebApi.Services;

using Microsoft.AspNet.Mvc;

namespace Apv.WebApi.Controllers
{
    [Route("api/members")]
    public class MemberController : Controller
    {
        private readonly MemberService _memberService;

        public MemberController(MemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: api/member
        [HttpGet]
        public IEnumerable<MemberDto> Get()
        {
            return _memberService.GetMembers();
        }

        // GET api/member/5
        [HttpGet("{id}")]
        public MemberDetailsDto Get(long id)
        {
            return _memberService.GetMemberDetails(id);
        }

        // PUT api/member
        [HttpPut]
        public void Put([FromBody]MemberDetailsDto detailsDto)
        {
            _memberService.UpdateMember(detailsDto);
        }
    }
}
