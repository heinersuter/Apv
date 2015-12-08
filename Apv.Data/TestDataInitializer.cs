using System;
using System.Data.Entity;

using Apv.Data.Model;

namespace Apv.Data
{
    public class TestDataInitializer : DropCreateDatabaseAlways<ApvDbContext>
    {
        protected override void Seed(ApvDbContext context)
        {
            var member = context.Members.Add(new Member { Nickname = "Hirsch" });
            member.Addresses.Add(new Address { Street = "Ackersteinstrasse 207" });

            member = context.Members.Add(new Member { Nickname = "Marabu" });
            member.Addresses.Add(new Address { Street = "Weg 23" });

            member = context.Members.Add(new Member { Nickname = "Bavi", Status = MemberStatus.Inactive });
            member.Birthdate = new DateTime(1934, 12, 30);

            base.Seed(context);
        }
    }
}
