namespace Model.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OnlineShopDbContext : DbContext
    {
        public OnlineShopDbContext()
            : base("name=OnlineShop")
        {
        }

        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<CatalogBrand> CatalogBrands { get; set; }
        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<CatalogType> CatalogTypes { get; set; }
        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Specification> Specifications { get; set; }
        public virtual DbSet<SpecificationsLaptop> SpecificationsLaptops { get; set; }
        public virtual DbSet<SpecificationsMobile> SpecificationsMobiles { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .HasOptional(e => e.Notification)
                .WithRequired(e => e.Bill)
                .WillCascadeOnDelete();
        }
    }
}
