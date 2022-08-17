using KarmaStore.Models;
using Microsoft.EntityFrameworkCore;

namespace KarmaStore.DTO
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext (DbContextOptions options) : base(options)
        {
           
        }
        #region DbSet

        public DbSet<DTO_Products> Products { get; set; }

        public DbSet<DTO_Category> Category { get; set; }
     
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DTO_Products>(e =>
            {
                e.ToTable("Products");
                e.Property(pro => pro.IsActive).HasDefaultValue(true);             
                e.Property(pro => pro.UpdatedAt).HasDefaultValue(null);
            });
        }
    }
   
}
