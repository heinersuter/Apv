using System.Collections.Generic;
using System.Net;

using Apv.Data.Dtos.Members;
using Apv.WebApi.Services;

using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Apv.WebApi.Controllers
{
    [Route("api/members")]
    [Authorize]
    public class MemberController : Controller
    {
        private readonly MemberService _memberService;

        public MemberController(MemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: api/members
        [HttpGet]
        public IEnumerable<MemberDto> Get()
        {
            return _memberService.GetMembers();
        }

        // GET api/members/5
        [HttpGet("{id}")]
        public MemberDetailsDto Get(long id)
        {
            var memberDetailsDto = _memberService.GetMemberDetails(id);
            if (memberDetailsDto == null)
            {
                throw new WebException("The provided ID is unkonwn.", WebExceptionStatus.UnknownError);
            }
            return memberDetailsDto;
        }

        // PUT api/members
        [HttpPut]
        public void Put([FromBody]MemberDetailsDto detailsDto)
        {
            if (detailsDto == null)
            {
                throw new WebException("A MemberDetailsDto must be provided as body.", WebExceptionStatus.UnknownError);
            }
            _memberService.UpdateMember(detailsDto);
        }
    }
}
