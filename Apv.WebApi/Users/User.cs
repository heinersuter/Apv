using Microsoft.AspNet.Identity.EntityFramework;

namespace Apv.WebApi.Users
{
    public class User : IdentityUser
    {
        public long MemberId { get; set; }
    }
}
