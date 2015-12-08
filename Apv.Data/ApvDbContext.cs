using System.Data.Entity;

using Apv.Data.Model;

namespace Apv.Data
{
    public class ApvDbContext : DbContext
    {
        public ApvDbContext()
            : base("ApvConnectionString")
        {
            Database.SetInitializer<ApvDbContext>(new TestDataInitializer());
        }

        public DbSet<Member> Members { get; set; }
    }
}
