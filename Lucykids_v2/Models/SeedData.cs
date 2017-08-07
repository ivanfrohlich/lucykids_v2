using Lucykids_v2.DAL;
using Lucykids_v2.Models;
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

            var zeroToThreeMonths = context.Sizes.Add(new Size { Name = "0-3 months old" }).Entity;
            var threeToSixMonths = context.Sizes.Add(new Size { Name = "3-6 months old" }).Entity;
            var sixToTwelveMonths = context.Sizes.Add(new Size { Name = "6-12 months old" }).Entity;
            var twelveToEighteenMonths = context.Sizes.Add(new Size { Name = "12-18 months old" }).Entity;
            var eighteenToTwentyfourMonths = context.Sizes.Add(new Size { Name = "18-24 months old" }).Entity;
            var twoTothreeYears = context.Sizes.Add(new Size { Name = "2-3 years old" }).Entity;
            var threeToFourYears = context.Sizes.Add(new Size { Name = "3-4 years old" }).Entity;
            var fourToFiveYears = context.Sizes.Add(new Size { Name = "4-5 years old" }).Entity;

            var next = context.Brands.Add(new Brand { Name = "Next" }).Entity;
            var disney = context.Brands.Add(new Brand { Name = "Disney Store" }).Entity;
            var matalan = context.Brands.Add(new Brand { Name = "Matalan" }).Entity;
            var sainsburys = context.Brands.Add(new Brand { Name = "Sainsburys" }).Entity;
            var tesco = context.Brands.Add(new Brand { Name = "Tesco" }).Entity;
            var george = context.Brands.Add(new Brand { Name = "George" }).Entity;



            if (!context.Products.Any())
            {
                context.Products.AddRange(
                new Product
                {
                    Name = "Frozen Dressing Gown",
                    Price = 3.20m,
                    Gender = Gender.Girls,
                    Size = threeToSixMonths,
                    Brand = next,
                },
                new Product
                {
                    Name = "Cream Flower Dress",
                    Price = 8.20m,
                    Gender = Gender.Girls,
                    Size = threeToSixMonths,
                    Brand = disney,
                },
                new Product
                {
                    Name = "Blue Spotty T-Shirt",
                    Price = 7.40m,
                    Gender = Gender.Girls,
                    Size = eighteenToTwentyfourMonths,
                    Brand = matalan,
                },
                new Product
                {
                    Name = "Navy Cardigan",
                    Price = 6.99m,
                    Gender = Gender.Girls,
                    Size = zeroToThreeMonths,
                    Brand= tesco,
                },
                new Product
                {
                    Name = "Brown Cord Trousers",
                    Price = 9.30m,
                    Gender = Gender.Boys,
                    Size = threeToSixMonths,
                    Brand = disney,
                },
                new Product
                {
                    Name = "Red Junior T-Shirt",
                    Price = 6.99m,
                    Gender = Gender.Boys,
                    Size = eighteenToTwentyfourMonths,
                    Brand = next,

                },
                new Product
                {
                    Name = "Pink Hand Kittet Cardigan",
                    Price = 1.60m,
                    Gender = Gender.Girls,
                    Size = twelveToEighteenMonths,
                    Brand = george,

                },
                new Product
                {
                    Name = "Blue Face Jumper",
                    Price = 3.00m,
                    Gender = Gender.Unisex,
                    Size = threeToSixMonths,
                    Brand = disney,
                },
                new Product
                {
                    Name = "Tiger Jeans",
                    Price = 3.30m,
                    Gender = Gender.Boys,
                    Size = twelveToEighteenMonths,
                    Brand = sainsburys,
                }
                );
             }  

            context.SaveChanges();
        }
    }
}
