using System.Data.Entity;

using Apv.Data.Model;

namespace Apv.Data
{
    public class ApvDbContext : DbContext
    {
        public ApvDbContext()
            : base("ApvConnectionString")
        {
            //Database.SetInitializer(new TestDataInitializer());
        }

        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Communication>().HasKey(t => t.MemberId);

            modelBuilder.Entity<Member>()
                .HasRequired(t => t.Communication)
                .WithRequiredPrincipal(t => t.Member);

            base.OnModelCreating(modelBuilder);
        }
    }
}
