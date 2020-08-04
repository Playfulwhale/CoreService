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
            modelBuilder.Entity<SystemSetting>().ToTable("SystemSettings");
            modelBuilder.Entity<SystemSettingGroup>().ToTable("SystemSettingGroups");
            modelBuilder.Entity<SlideContent>().ToTable("SlideContents");
            modelBuilder.Entity<Slide>().ToTable("Slides");
            modelBuilder.Entity<PaidPackage>().ToTable("PaidPackage");
            modelBuilder.Entity<PaidPackagePrice>().ToTable("PaidPackagePrice");
            modelBuilder.Entity<Country>().ToTable("Country");
            modelBuilder.Entity<Zone>().ToTable("Zone");
            modelBuilder.Entity<Language>().ToTable("Language");
            modelBuilder.Entity<PaymentMethod>().ToTable("PaymentMethod");
            modelBuilder.Entity<Currency>().ToTable("Currency");
            modelBuilder.Entity<Menu>().ToTable("Menu");
            modelBuilder.Entity<MenuItem>().ToTable("MenuItem");
            modelBuilder.Entity<MenuItemDescription>().ToTable("MenuItemDescription");
            modelBuilder.Entity<PackageSubscriber>().ToTable("PackageSubscriber");
            modelBuilder.Entity<Transaction>().ToTable("Transaction");
        }

        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<SystemSetting> SystemSettings { get; set; }
        public DbSet<SystemSettingGroup> SystemSettingGroups { get; set; }
        public DbSet<SlideContent> SlideContents { get; set; }
        public DbSet<Slide> Slides { get; set; }
        public DbSet<PaidPackagePrice> PaidPackagePrices { get; set; }
        public DbSet<PaidPackage> PaidPackages { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemDescription> MenuItemDescriptions { get; set; }
        public DbSet<PackageSubscriber> PackageSubscribers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}