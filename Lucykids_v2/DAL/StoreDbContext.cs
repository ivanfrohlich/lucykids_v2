using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucykids_v2.Models;
using Lucykids_v2.Models.ViewModels;

namespace Lucykids_v2.DAL
{
    public class StoreDbContext: DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductImageMapping> ProductImageMappings { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
       
       

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductImage>()
                .HasIndex(pi => pi.FileName)
                .IsUnique();
        }
    }
}
