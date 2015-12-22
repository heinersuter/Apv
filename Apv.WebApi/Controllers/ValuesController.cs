using System.Collections.Generic;
using System.Linq;

using Apv.WebApi.Services;

using Microsoft.AspNet.Mvc;

namespace Apv.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static readonly List<string> _valueStore = new List<string> { "value1", "value2" };

        private readonly MemberService _memberService = new MemberService();

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _memberService.GetMembers().Select(dto => dto.Nickname);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return _valueStore[id];
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            _valueStore.Add(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
