using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GiayDep.Models
{
    public partial class GiaydepContext : IdentityDbContext<AppUserModel>
    {
        public GiaydepContext()
        {
        }

        public GiaydepContext(DbContextOptions<GiaydepContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Brand> Brands { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrdersDetail> OrdersDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<ProductItem> ProductItems { get; set; } = null!;
        public virtual DbSet<ProductVariation> ProductVariations { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;
        public virtual DbSet<Suppiler> Suppilers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=NGHIANGHIA\\SQLSEVER2020EV;Initial Catalog=GIAYDEP2;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.BrandName).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color");

                entity.Property(e => e.ColorId).HasColumnName("ColorID");

                entity.Property(e => e.ColorHex).HasMaxLength(50);

                entity.Property(e => e.ColorName).HasMaxLength(50);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.CreateDate).HasColumnType("date");

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Import_Note_Suppiler");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.InvoiceId })
                    .HasName("PK_CT_PHIEU_NHAP");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImportNote_Details_Import_Note");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ImportNote_Details_Product_Items");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(450)
                    .HasColumnName("CustomerID");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_AspNetUsers");
            });

            modelBuilder.Entity<OrdersDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK_Invoice_Details");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Details_Invoice");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrdersDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Invoice_Details_Product_Items");
            });

            modelBuilder.Entity<Product>(entity =>
            {

                entity.HasKey(e => e.ProductId);

                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductName).HasMaxLength(50);

                entity.HasOne(d => d.BrandNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.Brand)
                    .HasConstraintName("FK_Product_Brand");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_Category");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("ImageId");

                entity.Property(e => e.ImageId);
                entity.Property(e => e.ImageUrl).HasColumnName("imageUrl");

                entity.HasOne(d => d.ProductItems)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductItemsId)
                    .HasConstraintName("FK_Product_Images_Product_Items");
            });

            modelBuilder.Entity<ProductItem>(entity =>
            {
                entity.HasKey(e => e.ProductItemsId)
                    .HasName("PK_Product_Items");

                entity.Property(e => e.ProductItemsId)
     
                    .HasColumnName("ProductItemsID");

                entity.Property(e => e.ColorId).HasColumnName("ColorID");

                entity.Property(e => e.ProductCode).HasMaxLength(50);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ProductItems)
                    .HasForeignKey(d => d.ColorId)
                    .HasConstraintName("FK_Product_Items_Color");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Product_Items_Product");
            });

            modelBuilder.Entity<ProductVariation>(entity =>
            {
                entity.HasKey(e => e.VariationId)
                    .HasName("PK_Product_Variation");

                entity.ToTable("ProductVariation");

                entity.Property(e => e.VariationId)
                    .HasColumnName("VariationID");

                entity.Property(e => e.ProductItemsId).HasColumnName("ProductItemsID");

                entity.Property(e => e.SizeId).HasColumnName("SizeID");

                entity.HasOne(d => d.ProductItems)
                    .WithMany(p => p.ProductVariations)
                    .HasForeignKey(d => d.ProductItemsId)
                    .HasConstraintName("FK_Product_Variation_Product_Items1");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.ProductVariations)
                    .HasForeignKey(d => d.SizeId)
                    .HasConstraintName("FK_Product_Variation_Size");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.HasKey(e => e.SizeID);

                entity.ToTable("Size");

                entity.Property(e => e.Size1).HasColumnName("Size");
            });

            modelBuilder.Entity<Suppiler>(entity =>
            {
                entity.HasKey(e => e.SupplierId);

                entity.ToTable("Suppiler");

                entity.Property(e => e.SupplierId).HasColumnName("SuppilerID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(30);

                entity.Property(e => e.SupplierName).HasMaxLength(50);
            });

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
