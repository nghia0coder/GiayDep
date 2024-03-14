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

  
        public virtual DbSet<ChitietSanPham> ChitietSanPhams { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<CtHoaDon> CtHoaDons { get; set; } = null!;
        public virtual DbSet<CtPhieuNhap> CtPhieuNhaps { get; set; } = null!;
        public virtual DbSet<HoaDon> HoaDons { get; set; } = null!;
        public virtual DbSet<LoaiSp> LoaiSps { get; set; } = null!;
        public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; } = null!;
        public virtual DbSet<NhaSanXuat> NhaSanXuats { get; set; } = null!;
        public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; } = null!;
        public virtual DbSet<SanPham> SanPhams { get; set; } = null!;
        public virtual DbSet<Size> Sizes { get; set; } = null!;
        public virtual DbSet<SoLuongTon> SoLuongTons { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=NGHIANGHIA\\SQLSEVER2020EV;Initial Catalog=QLGIAYDEP;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            modelBuilder.Entity<ChitietSanPham>(entity =>
            {
                entity.HasKey(e => new { e.Idsp, e.Idcolor, e.Idsize });

                entity.ToTable("ChitietSanPham");

                entity.Property(e => e.Idsp).HasColumnName("IDSP");

                entity.Property(e => e.Idcolor).HasColumnName("IDColor");

                entity.Property(e => e.Idsize).HasColumnName("IDSize");

                entity.HasOne(d => d.IdcolorNavigation)
                    .WithMany(p => p.ChitietSanPhams)
                    .HasForeignKey(d => d.Idcolor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChitietSanPham_Color");

                entity.HasOne(d => d.IdsizeNavigation)
                    .WithMany(p => p.ChitietSanPhams)
                    .HasForeignKey(d => d.Idsize)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChitietSanPham_Size");

                entity.HasOne(d => d.IdspNavigation)
                    .WithMany(p => p.ChitietSanPhams)
                    .HasForeignKey(d => d.Idsp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ChitietSanPham_SAN_PHAM");
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.ToTable("Color");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<CtHoaDon>(entity =>
            {
                entity.HasKey(e => e.IdchitietDdh)
                    .HasName("PK_CT_HOA_DON_1");

                entity.ToTable("CT_HOA_DON");

                entity.Property(e => e.IdchitietDdh).HasColumnName("IDchitietDDH");

                entity.Property(e => e.Dongia).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Idhoadon).HasColumnName("IDhoadon");

                entity.Property(e => e.Idsp).HasColumnName("IDsp");

                entity.Property(e => e.Tensp).HasMaxLength(50);

                entity.HasOne(d => d.IdhoadonNavigation)
                    .WithMany(p => p.CtHoaDons)
                    .HasForeignKey(d => d.Idhoadon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CT_HOA_DON_HOA_DON");

                entity.HasOne(d => d.IdspNavigation)
                    .WithMany(p => p.CtHoaDons)
                    .HasForeignKey(d => d.Idsp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CT_HOA_DON_SAN_PHAM");
            });
            modelBuilder.Entity<CtPhieuNhap>(entity =>
            {
                entity.HasKey(e => new { e.Idsp, e.Idphieunhap });

                entity.ToTable("CT_PHIEU_NHAP");

                entity.Property(e => e.Idsp).HasColumnName("IDSP");

                entity.Property(e => e.Idphieunhap).HasColumnName("IDphieunhap");

                entity.Property(e => e.Gia).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.IdphieunhapNavigation)
                    .WithMany(p => p.CtPhieuNhaps)
                    .HasForeignKey(d => d.Idphieunhap)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CT_PHIEU_NHAP_PHIEU_NHAP1");

                entity.HasOne(d => d.IdspNavigation)
                    .WithMany(p => p.CtPhieuNhaps)
                    .HasForeignKey(d => d.Idsp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CT_PHIEU_NHAP_SAN_PHAM");
            });

            modelBuilder.Entity<HoaDon>(entity =>
            {
                entity.HasKey(e => e.Idhoadon);

                entity.ToTable("HOA_DON");

                entity.Property(e => e.Idhoadon).HasColumnName("IDhoadon");

                entity.Property(e => e.Makh).HasMaxLength(450);

                entity.HasOne(d => d.MakhNavigation)
                    .WithMany(p => p.HoaDons)
                    .HasForeignKey(d => d.Makh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HOA_DON_AspNetUsers");
            });

            modelBuilder.Entity<LoaiSp>(entity =>
            {
                entity.HasKey(e => e.Idloai);

                entity.ToTable("LoaiSp");

                entity.Property(e => e.Tenloai).HasMaxLength(50);
            });

            modelBuilder.Entity<NhaCungCap>(entity =>
            {
                entity.HasKey(e => e.Idnhacc);

                entity.ToTable("NHA_CUNG_CAP");

                entity.Property(e => e.Idnhacc).HasColumnName("IDnhacc");

                entity.Property(e => e.Diachi).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Idnhasx).HasColumnName("IDnhasx");

                entity.Property(e => e.Sdt).HasMaxLength(30);

                entity.Property(e => e.Tennhacc).HasMaxLength(50);

                entity.HasOne(d => d.IdnhasxNavigation)
                    .WithMany(p => p.NhaCungCaps)
                    .HasForeignKey(d => d.Idnhasx)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NHA_CUNG_CAP_NHA_SAN_XUAT");
            });

            modelBuilder.Entity<NhaSanXuat>(entity =>
            {
                entity.HasKey(e => e.Idnhasx);

                entity.ToTable("NHA_SAN_XUAT");

                entity.Property(e => e.Idnhasx).HasColumnName("IDnhasx");

                entity.Property(e => e.Diachi).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Sđt).HasMaxLength(30);

                entity.Property(e => e.Tennhasx).HasMaxLength(50);
            });

            modelBuilder.Entity<PhieuNhap>(entity =>
            {
                entity.HasKey(e => e.Idphieunhap);

                entity.ToTable("PHIEU_NHAP");

                entity.Property(e => e.Idphieunhap).HasColumnName("IDphieunhap");

                entity.Property(e => e.Idnhacc).HasColumnName("IDnhacc");

                entity.HasOne(d => d.IdnhaccNavigation)
                    .WithMany(p => p.PhieuNhaps)
                    .HasForeignKey(d => d.Idnhacc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PHIEU_NHAP_NHA_CUNG_CAP");
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasKey(e => e.Idsp);

                entity.ToTable("SAN_PHAM");

                entity.Property(e => e.Idsp).HasColumnName("IDSP");

                entity.Property(e => e.Baohanh).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Khuyenmai).HasMaxLength(50);

                entity.Property(e => e.Tensp).HasMaxLength(50);

                entity.HasOne(d => d.MaloaispNavigation)
                        .WithMany(p => p.SanPhams)
                        .HasForeignKey(d => d.Maloaisp)
                        .HasConstraintName("FK_SAN_PHAM_LoaiSp");

                entity.HasOne(d => d.ManhaccNavigation)
                    .WithMany(p => p.SanPhams)
                    .HasForeignKey(d => d.Manhacc)
                    .HasConstraintName("FK_SAN_PHAM_NHA_CUNG_CAP");
            });

            modelBuilder.Entity<Size>(entity =>
            {
                entity.ToTable("Size");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<SoLuongTon>(entity =>
            {
                entity.HasKey(e => e.Idsp)
                    .HasName("PK_SoLuongTon_1");

                entity.ToTable("SoLuongTon");

                entity.Property(e => e.Idsp)
                    .ValueGeneratedNever()
                    .HasColumnName("IDSP");

                entity.Property(e => e.Soluongton1).HasColumnName("Soluongton");

                entity.HasOne(d => d.IdspNavigation)
                    .WithOne(p => p.SoLuongTon)
                    .HasForeignKey<SoLuongTon>(d => d.Idsp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SoLuongTon_SAN_PHAM");
            });

            base.OnModelCreating(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
