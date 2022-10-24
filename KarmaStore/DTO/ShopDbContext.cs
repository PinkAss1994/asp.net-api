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
        public DbSet<DTO_User> Users { get; set; }
        public DbSet<DTO_Category> Category { get; set; }
        public DbSet<DTO_Role> Role { get; set; }
     
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DTO_Products>(e =>
            {
                e.ToTable("Products");
                e.Property(pro => pro.IsActive).HasDefaultValue(true);             
                e.Property(pro => pro.UpdatedAt).HasDefaultValue(null);
            });
            modelBuilder.Entity<DTO_User>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Role)
                   .HasMaxLength(50)
                   .IsUnicode(false);
            });
            modelBuilder.Entity<DTO_Role>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("Role");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RoleId");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
   
}
