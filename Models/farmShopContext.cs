using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace progPart2.Models
{
    public partial class farmShopContext:DbContext
    {
        public farmShopContext()
        {
        }
        public farmShopContext(DbContextOptions<farmShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Sales> Sales { get; set; }
        public virtual DbSet<Users> Users { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=farmShop;Integrated Security=True");
            }
        }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK__Products__B40CC6EDC7BD1C6B");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductDesc)
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.ProductPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Productcategory)
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.ProductImage).HasColumnType("varbinary(max)");

            });

            modelBuilder.Entity<Sales>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.ProductId })
                    .HasName("PK__Sales__982C498B6B352F7C");

                entity.Property(e => e.Username)
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sales__ProductID__286302EC");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sales__Username__29572725");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__Users__536C85E5A08C071F");

                entity.Property(e => e.Username)
                    .HasMaxLength(225)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(225)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
