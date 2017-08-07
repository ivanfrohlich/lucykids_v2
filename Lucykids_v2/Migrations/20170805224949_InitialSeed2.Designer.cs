using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Lucykids_v2.DAL;
using Lucykids_v2.Models;

namespace Lucykids_v2.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    [Migration("20170805224949_InitialSeed2")]
    partial class InitialSeed2
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

            modelBuilder.Entity("Lucykids_v2.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BrandId");

                    b.Property<int>("Gender");

                    b.Property<string>("Name");

                    b.Property<decimal?>("Price");

                    b.Property<int?>("SizeId");

                    b.HasKey("ProductId");

                    b.HasIndex("BrandId");

                    b.HasIndex("SizeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Lucykids_v2.Models.Size", b =>
                {
                    b.Property<int>("SizeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("SizeId");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("Lucykids_v2.Models.Product", b =>
                {
                    b.HasOne("Lucykids_v2.Models.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId");

                    b.HasOne("Lucykids_v2.Models.Size", "Size")
                        .WithMany("Products")
                        .HasForeignKey("SizeId");
                });
        }
    }
}
