using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Apv.Data.Model;

namespace Apv.Data
{
    public class ApvDataAccess
    {
        public static IEnumerable<Member> GetActiveMembers()
        {
            using (var context = new ApvDbContext())
            {
                return context.Members.Include(member => member.Addresses).ToArray();
            }
        }

        public static void UpdateMember(Member member)
        {
            using (var context = new ApvDbContext())
            {
                context.Members.Attach(member);
                var entry = context.Entry(member);
                entry.State = EntityState.Modified;
                context.SaveChanges();
            }

        }
    }
}
