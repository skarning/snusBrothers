using System.Data.Entity;
using Snusbrother.Models.Models;

namespace Snusbrother.DataAccess
{
    public class SnusbrotherDataContext : DbContext
    {
       public SnusbrotherDataContext() : base("name = sivertsk")
        {

            //this.Configuration.ProxyCreationEnabled = false;
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Snus> Snus { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Snus>()
                .HasRequired<Profile>(s => s.Profile)
                .WithMany()
                .WillCascadeOnDelete(true);

             modelBuilder.Entity<Request>()
                .HasRequired<Profile>(r => r.Profile)
                .WithMany()
                .WillCascadeOnDelete(true);
        }
    }
}
