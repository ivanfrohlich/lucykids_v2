using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Lucykids_v2.DAL;

namespace Lucykids_v2.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20170831220615_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lucykids_v2.Models.Brand", b =>
                {
                    b.Property<int>("BrandId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("BrandId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("Lucykids_v2.Models.CartLine", b =>
                {
                    b.Property<int>("CartLineId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CartId");

                    b.Property<int?>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("CartLineId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartLines");
                });

            modelBuilder.Entity("Lucykids_v2.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Lucykids_v2.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("AddressLine2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("OrderPlaced");

                    b.Property<decimal>("OrderTotal");

                    b.Property<string>("State")
                        .HasMaxLength(10);

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("OrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Lucykids_v2.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OrderId");

                    b.Property<int?>("PieProductId");

                    b.Property<decimal>("Price");

                    b.Property<int>("ProuctId");

                    b.Property<int>("Quantity");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("PieProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("Lucykids_v2.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BrandId");

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int?>("SizeId");

                    b.HasKey("ProductId");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SizeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Lucykids_v2.Models.ProductImage", b =>
                {
                    b.Property<int>("ProductImageId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName");

                    b.Property<int?>("ProductId");

                    b.HasKey("ProductImageId");

                    b.HasIndex("FileName")
                        .IsUnique();

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("Lucykids_v2.Models.ProductImageMapping", b =>
                {
                    b.Property<int>("ProductImageMappingId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CartLineId");

                    b.Property<int>("ImageNumber");

                    b.Property<int>("ProductId");

                    b.Property<int>("ProductImageId");

                    b.HasKey("ProductImageMappingId");

                    b.HasIndex("CartLineId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductImageId");

                    b.ToTable("ProductImageMappings");
                });

            modelBuilder.Entity("Lucykids_v2.Models.Size", b =>
                {
                    b.Property<int>("SizeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("SizeId");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("Lucykids_v2.Models.CartLine", b =>
                {
                    b.HasOne("Lucykids_v2.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Lucykids_v2.Models.OrderDetail", b =>
                {
                    b.HasOne("Lucykids_v2.Models.Order", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lucykids_v2.Models.Product", "Pie")
                        .WithMany()
                        .HasForeignKey("PieProductId");
                });

            modelBuilder.Entity("Lucykids_v2.Models.Product", b =>
                {
                    b.HasOne("Lucykids_v2.Models.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId");

                    b.HasOne("Lucykids_v2.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("Lucykids_v2.Models.Size", "Size")
                        .WithMany("Products")
                        .HasForeignKey("SizeId");
                });

            modelBuilder.Entity("Lucykids_v2.Models.ProductImage", b =>
                {
                    b.HasOne("Lucykids_v2.Models.Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Lucykids_v2.Models.ProductImageMapping", b =>
                {
                    b.HasOne("Lucykids_v2.Models.CartLine")
                        .WithMany("ProductImageMappings")
                        .HasForeignKey("CartLineId");

                    b.HasOne("Lucykids_v2.Models.Product", "Product")
                        .WithMany("ProductImageMappings")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Lucykids_v2.Models.ProductImage", "ProductImage")
                        .WithMany("ProductImageMappings")
                        .HasForeignKey("ProductImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
