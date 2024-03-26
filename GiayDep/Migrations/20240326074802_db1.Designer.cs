﻿// <auto-generated />
using System;
using GiayDep.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GiayDep.Migrations
{
    [DbContext(typeof(GiaydepContext))]
    [Migration("20240326074802_db1")]
    partial class db1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GiayDep.Models.AppUserModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("GiayDep.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BrandID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandId"), 1L, 1);

                    b.Property<string>("BrandName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BrandId");

                    b.ToTable("Brand", (string)null);
                });

            modelBuilder.Entity("GiayDep.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"), 1L, 1);

                    b.Property<string>("CategoryName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("GiayDep.Models.Color", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ColorID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ColorId"), 1L, 1);

                    b.Property<string>("ColorHex")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ColorName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ColorId");

                    b.ToTable("Color", (string)null);
                });

            modelBuilder.Entity("GiayDep.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("InvoiceID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InvoiceId"), 1L, 1);

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("SupplierId")
                        .HasColumnType("int")
                        .HasColumnName("SupplierID");

                    b.HasKey("InvoiceId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Invoice", (string)null);
                });

            modelBuilder.Entity("GiayDep.Models.InvoiceDetail", b =>
                {
                    b.Property<int>("ProductVarId")
                        .HasColumnType("int")
                        .HasColumnName("ProductVarID");

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int")
                        .HasColumnName("InvoiceID");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("Quanity")
                        .HasColumnType("int");

                    b.HasKey("ProductVarId", "InvoiceId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProductVarId")
                        .IsUnique();

                    b.ToTable("InvoiceDetails");
                });

            modelBuilder.Entity("GiayDep.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasMaxLength(450)
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("CustomerID");

                    b.Property<bool>("Delivered")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("GiayDep.Models.OrdersDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<int>("ProductVarId")
                        .HasColumnType("int")
                        .HasColumnName("ProductVarID");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<int?>("Quanity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductVarId")
                        .HasName("PK_OrdersDetails_1");

                    b.HasIndex("ProductVarId");

                    b.ToTable("OrdersDetails");
                });

            modelBuilder.Entity("GiayDep.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<int?>("Brand")
                        .HasColumnType("int");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("CategoryID");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ProductId");

                    b.HasIndex("Brand");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("GiayDep.Models.ProductItem", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int>("ColorId")
                        .HasColumnType("int")
                        .HasColumnName("ColorID");

                    b.Property<string>("Image1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ProductItemsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductItemsID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductItemsId"), 1L, 1);

                    b.HasKey("ProductId", "ColorId")
                        .HasName("PK_ProductItems_1");

                    b.HasIndex("ColorId");

                    b.HasIndex(new[] { "ProductItemsId" }, "IX_ProductItems")
                        .IsUnique();

                    b.HasIndex(new[] { "ProductCode" }, "ProductCode")
                        .IsUnique()
                        .HasFilter("[ProductCode] IS NOT NULL");

                    b.ToTable("ProductItems");
                });

            modelBuilder.Entity("GiayDep.Models.ProductVariation", b =>
                {
                    b.Property<int>("ProductItemsId")
                        .HasColumnType("int")
                        .HasColumnName("ProductItemsID");

                    b.Property<int>("SizeId")
                        .HasColumnType("int")
                        .HasColumnName("SizeID");

                    b.Property<int>("ProductVarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ProductVarID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductVarId"), 1L, 1);

                    b.Property<int?>("QtyinStock")
                        .HasColumnType("int");

                    b.HasKey("ProductItemsId", "SizeId");

                    b.HasIndex("SizeId");

                    b.HasIndex(new[] { "ProductVarId" }, "IX_ProductVariation")
                        .IsUnique();

                    b.ToTable("ProductVariation", (string)null);
                });

            modelBuilder.Entity("GiayDep.Models.Size", b =>
                {
                    b.Property<int>("SizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SizeID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SizeId"), 1L, 1);

                    b.Property<int?>("Size1")
                        .HasColumnType("int")
                        .HasColumnName("Size");

                    b.HasKey("SizeId");

                    b.ToTable("Size", (string)null);
                });

            modelBuilder.Entity("GiayDep.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("SupplierID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SupplierId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("SupplierId");

                    b.ToTable("Supplier", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("GiayDep.Models.Invoice", b =>
                {
                    b.HasOne("GiayDep.Models.Supplier", "Supplier")
                        .WithMany("Invoices")
                        .HasForeignKey("SupplierId")
                        .IsRequired()
                        .HasConstraintName("FK_Import_Note_Suppiler");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("GiayDep.Models.InvoiceDetail", b =>
                {
                    b.HasOne("GiayDep.Models.Invoice", "Invoice")
                        .WithMany("InvoiceDetails")
                        .HasForeignKey("InvoiceId")
                        .IsRequired()
                        .HasConstraintName("FK_InvoiceDetails_Invoice1");

                    b.HasOne("GiayDep.Models.ProductVariation", "ProductVar")
                        .WithOne("InvoiceDetail")
                        .HasForeignKey("GiayDep.Models.InvoiceDetail", "ProductVarId")
                        .HasPrincipalKey("GiayDep.Models.ProductVariation", "ProductVarId")
                        .IsRequired()
                        .HasConstraintName("FK_InvoiceDetails_ProductVariation");

                    b.Navigation("Invoice");

                    b.Navigation("ProductVar");
                });

            modelBuilder.Entity("GiayDep.Models.Order", b =>
                {
                    b.HasOne("GiayDep.Models.AppUserModel", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .IsRequired()
                        .HasConstraintName("FK_Invoice_AspNetUsers");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("GiayDep.Models.OrdersDetail", b =>
                {
                    b.HasOne("GiayDep.Models.Order", "Order")
                        .WithMany("OrdersDetails")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_Invoice_Details_Invoice");

                    b.HasOne("GiayDep.Models.ProductVariation", "ProductVar")
                        .WithMany("OrdersDetails")
                        .HasForeignKey("ProductVarId")
                        .HasPrincipalKey("ProductVarId")
                        .IsRequired()
                        .HasConstraintName("FK_OrdersDetails_ProductVariation");

                    b.Navigation("Order");

                    b.Navigation("ProductVar");
                });

            modelBuilder.Entity("GiayDep.Models.Product", b =>
                {
                    b.HasOne("GiayDep.Models.Brand", "BrandNavigation")
                        .WithMany("Products")
                        .HasForeignKey("Brand")
                        .HasConstraintName("FK_Product_Brand");

                    b.HasOne("GiayDep.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Product_Category");

                    b.Navigation("BrandNavigation");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("GiayDep.Models.ProductItem", b =>
                {
                    b.HasOne("GiayDep.Models.Color", "Color")
                        .WithMany("ProductItems")
                        .HasForeignKey("ColorId")
                        .IsRequired()
                        .HasConstraintName("FK_ProductItems_Color");

                    b.HasOne("GiayDep.Models.Product", "Product")
                        .WithMany("ProductItems")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_ProductItems_Product");

                    b.Navigation("Color");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("GiayDep.Models.ProductVariation", b =>
                {
                    b.HasOne("GiayDep.Models.ProductItem", "ProductItems")
                        .WithMany("ProductVariations")
                        .HasForeignKey("ProductItemsId")
                        .HasPrincipalKey("ProductItemsId")
                        .IsRequired()
                        .HasConstraintName("FK_ProductVariation_ProductItems");

                    b.HasOne("GiayDep.Models.Size", "Size")
                        .WithMany("ProductVariations")
                        .HasForeignKey("SizeId")
                        .IsRequired()
                        .HasConstraintName("FK_ProductVariation_Size");

                    b.Navigation("ProductItems");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("GiayDep.Models.AppUserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("GiayDep.Models.AppUserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GiayDep.Models.AppUserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("GiayDep.Models.AppUserModel", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GiayDep.Models.AppUserModel", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("GiayDep.Models.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("GiayDep.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("GiayDep.Models.Color", b =>
                {
                    b.Navigation("ProductItems");
                });

            modelBuilder.Entity("GiayDep.Models.Invoice", b =>
                {
                    b.Navigation("InvoiceDetails");
                });

            modelBuilder.Entity("GiayDep.Models.Order", b =>
                {
                    b.Navigation("OrdersDetails");
                });

            modelBuilder.Entity("GiayDep.Models.Product", b =>
                {
                    b.Navigation("ProductItems");
                });

            modelBuilder.Entity("GiayDep.Models.ProductItem", b =>
                {
                    b.Navigation("ProductVariations");
                });

            modelBuilder.Entity("GiayDep.Models.ProductVariation", b =>
                {
                    b.Navigation("InvoiceDetail");

                    b.Navigation("OrdersDetails");
                });

            modelBuilder.Entity("GiayDep.Models.Size", b =>
                {
                    b.Navigation("ProductVariations");
                });

            modelBuilder.Entity("GiayDep.Models.Supplier", b =>
                {
                    b.Navigation("Invoices");
                });
#pragma warning restore 612, 618
        }
    }
}
