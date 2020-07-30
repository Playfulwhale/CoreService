using Microsoft.EntityFrameworkCore;
namespace ApiTemplate.Data
{
    using Models;

    public class DataContext : DbContext
    {
        public DbContextOptions<DataContext> Options { get; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Options = options;
        }

        public DataContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            modelBuilder.Entity<ContactUs>().ToTable("Contacts");
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Zone>().ToTable("Zone");
        }

        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Zone> Zones { get; set; }
    }
}