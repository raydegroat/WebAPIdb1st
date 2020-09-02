using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPIdb1st.Models
{
    public partial class ApiContext : DbContext
    {
        public ApiContext()
        {
        }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BasketItems> BasketItems { get; set; }
        public virtual DbSet<Baskets> Baskets { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }
        public virtual DbSet<ProductCategories> ProductCategories { get; set; }
        public virtual DbSet<Products> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasketItems>(entity =>
            {
                entity.HasIndex(e => e.BasketId)
                    .HasName("IX_BasketId");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.BasketId).HasMaxLength(128);

                entity.HasOne(d => d.Basket)
                    .WithMany(p => p.BasketItems)
                    .HasForeignKey(d => d.BasketId)
                    .HasConstraintName("FK_dbo.BasketItems_dbo.Baskets_BasketId");
            });

            modelBuilder.Entity<Baskets>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<ProductCategories>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
