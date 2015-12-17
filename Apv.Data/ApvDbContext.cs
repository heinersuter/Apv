using System.Data.Entity;

using Apv.Data.Model;

namespace Apv.Data
{
    internal class ApvDbContext : DbContext
    {
        public ApvDbContext()
            : base("ApvConnectionString")
        {
            //Database.SetInitializer(new TestDataInitializer());
        }

        public DbSet<Member> Members { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<EmailAddress> EmailAddresses { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public DbSet<Note> Notes { get; set; }

        public DbSet<Function> Functions { get; set; }

        public DbSet<Communication> Communications { get; set; }

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
