using Lucykids_v2.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Lucykids_v2.Models
{
    public static class SeedData
    {
        /*
         * The static EnsurePopulated method receives an IApplictionBuilder argument,
         * which is the class used in the Configure method of the startup class to register
         * middleware classes to handle HTTP requests, which is where I will ensure that the database 
         * has content.
         */
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices.GetRequiredService<StoreDbContext>();

            if (context.Products.Any())
            {
                return; //Db has been seeded
            }

            var boys = context.Categories.Add(new Category { Name = "boys" }).Entity;
            var girls = context.Categories.Add(new Category { Name = "girls" }).Entity;
            var unisex = context.Categories.Add(new Category { Name = "unisex" }).Entity;
            context.SaveChanges();

            var zeroToThreeMonths = context.Sizes.Add(new Size { Name = "0-3 months old" }).Entity;
            var threeToSixMonths = context.Sizes.Add(new Size { Name = "3-6 months old" }).Entity;
            var sixToTwelveMonths = context.Sizes.Add(new Size { Name = "6-12 months old" }).Entity;
            var twelveToEighteenMonths = context.Sizes.Add(new Size { Name = "12-18 months old" }).Entity;
            var eighteenToTwentyfourMonths = context.Sizes.Add(new Size { Name = "18-24 months old" }).Entity;
            var twoTothreeYears = context.Sizes.Add(new Size { Name = "2-3 years old" }).Entity;
            var threeToFourYears = context.Sizes.Add(new Size { Name = "3-4 years old" }).Entity;
            var fourToFiveYears = context.Sizes.Add(new Size { Name = "4-5 years old" }).Entity;
            context.SaveChanges();

            var next = context.Brands.Add(new Brand { Name = "Next" }).Entity;
            var disney = context.Brands.Add(new Brand { Name = "Disney Store" }).Entity;
            var matalan = context.Brands.Add(new Brand { Name = "Matalan" }).Entity;
            var sainsburys = context.Brands.Add(new Brand { Name = "Sainsburys" }).Entity;
            var tesco = context.Brands.Add(new Brand { Name = "Tesco" }).Entity;
            var george = context.Brands.Add(new Brand { Name = "George" }).Entity;
            var unbranded = context.Brands.Add(new Brand { Name = "No Brand" }).Entity;
            context.SaveChanges();

            // products seed data, for image mapping to work must be created
            if (!context.Products.Any())
            {
                context.Products.AddRange(
                new Product
                {
                    Name = "Green tracksuit",
                    Price = 3.20m,
                    Category = boys,
                    Size = zeroToThreeMonths,
                    Brand = unbranded,
                },
                new Product
                {
                    Name = "Shoes small baby azure",
                    Price = 8.20m,
                    Category = boys,
                    Size = threeToSixMonths,
                    Brand = disney,
                },
                new Product
                {
                    Name = "Blue Jacket with colours",
                    Price = 7.40m,
                    Category = girls,
                    Size = eighteenToTwentyfourMonths,
                    Brand = matalan,
                },
                new Product
                {
                    Name = "Blue hat",
                    Price = 6.99m,
                    Category = boys,
                    Size = zeroToThreeMonths,
                    Brand = tesco,
                }
                //new Product
                //{
                //    Name = "Brown Cord Trousers",
                //    Price = 9.30m,
                //    Category = unisex,
                //    Size = threeToSixMonths,
                //    Brand = disney,
                //},
                //new Product
                //{
                //    Name = "Red Junior T-Shirt",
                //    Price = 6.99m,
                //    Category = boys,
                //    Size = eighteenToTwentyfourMonths,
                //    Brand = next,

                //},
                //new Product
                //{
                //    Name = "Pink Hand Kittet Cardigan",
                //    Price = 1.60m,
                //    Category = girls,
                //    Size = twelveToEighteenMonths,
                //    Brand = george,

                //},
                //new Product
                //{
                //    Name = "Blue Face Jumper",
                //    Price = 3.00m,
                //    Category = unisex,
                //    Size = threeToSixMonths,
                //    Brand = disney,
                //},
                //new Product
                //{
                //    Name = "Tiger Jeans",
                //    Price = 3.30m,
                //    Category = unisex,
                //    Size = twelveToEighteenMonths,
                //    Brand = sainsburys,
                //}
                );
            }
            context.SaveChanges();

            // test data for images
            if (!context.ProductImages.Any())
            {
                context.ProductImages.AddRange(

                new ProductImage { FileName = "blueHat.jpg" },
                new ProductImage { FileName = "blueHat1.jpg" },
                new ProductImage { FileName = "blueHat2.jpg" },
                new ProductImage { FileName = "blueJacketWithColours.jpg" },
                new ProductImage { FileName = "blueJacketWithColours1.jpg" },
                new ProductImage { FileName = "blueJacketWithColours2.jpg" },
                new ProductImage { FileName = "greenTracksuit.jpg" },
                new ProductImage { FileName = "greenTracksuit1.jpg" },
                new ProductImage { FileName = "greenTracksuit2.jpg" },
                new ProductImage { FileName = "greenTracksuit3.jpg" },
                new ProductImage { FileName = "greenTracksuit4.jpg" },
                new ProductImage { FileName = "shoesAzure.jpg" },
                new ProductImage { FileName = "shoesAzure1.jpg" },
                new ProductImage { FileName = "shoesAzure2.jpg" },
                new ProductImage { FileName = "shoesAzure3.jpg" }
                );
            }
            context.SaveChanges();

            // test data for product image mapping
            if (!context.ProductImageMappings.Any())
            {
                context.ProductImageMappings.Add(
                     new ProductImageMapping 
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "blueHat.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Blue hat").ProductId,
                        ImageNumber = 0
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "blueHat1.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Blue hat").ProductId,
                        ImageNumber = 1
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "blueHat2.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Blue hat").ProductId,
                        ImageNumber = 2
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "blueJacketWithColours.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Blue Jacket with colours").ProductId,
                        ImageNumber = 0
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "blueJacketWithColours1.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Blue Jacket with colours").ProductId,
                        ImageNumber = 1
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "blueJacketWithColours2.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Blue Jacket with colours").ProductId,
                        ImageNumber = 2
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "greenTracksuit.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Green tracksuit").ProductId,
                        ImageNumber = 0
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "greenTracksuit1.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Green tracksuit").ProductId,
                        ImageNumber = 1
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "greenTracksuit2.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Green tracksuit").ProductId,
                        ImageNumber = 2
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "greenTracksuit3.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Green tracksuit").ProductId,
                        ImageNumber = 3
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "greenTracksuit4.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Green tracksuit").ProductId,
                        ImageNumber = 4
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "shoesAzure.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Shoes small baby azure").ProductId,
                        ImageNumber = 0
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "shoesAzure1.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Shoes small baby azure").ProductId,
                        ImageNumber = 1
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "shoesAzure2.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Shoes small baby azure").ProductId,
                        ImageNumber = 2
                    });
                context.ProductImageMappings.Add(
                    new ProductImageMapping
                    {
                        ProductImageId = context.ProductImages.Single(i => i.FileName == "shoesAzure3.jpg").ProductImageId,
                        ProductId = context.Products.Single(p => p.Name == "Shoes small baby azure").ProductId,
                        ImageNumber = 3
                    });
            }
            context.SaveChanges();
           
        }
    }
}
