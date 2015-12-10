using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Apv.Data.Model;

namespace Apv.Data
{
    public class ApvDataAccess
    {
        public static IEnumerable<Member> GetMembers()
        {
            using (var context = new ApvDbContext())
            {
                return context.Members
                    .OrderBy(member => member.Nickname)
                    .ThenBy(member => member.Lastname)
                    .ThenBy(member => member.Firstname)
                    .ToArray();
            }
        }

        public static Member GetMemberDetails(long memberId)
        {
            using (var context = new ApvDbContext())
            {
                return context.Members.Where(m => m.Id == memberId)
                    .Include(m => m.PhoneNumbers)
                    .Include(m => m.EmailAddresses)
                    .Include(m => m.Addresses)
                    .Include(m => m.Notes)
                    .Include(m => m.Functions)
                    .Include(m => m.Communication)
                    .SingleOrDefault();
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
