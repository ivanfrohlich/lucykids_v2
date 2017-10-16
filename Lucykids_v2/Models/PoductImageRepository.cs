using Lucykids_v2.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Models
{
    public class PoductImageRepository : IProductImageRepository
    {
        private readonly StoreDbContext _storeDbContext;

        public PoductImageRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public IEnumerable<ProductImage> ProductImages
        {
            get
            {
                return _storeDbContext.ProductImages.Include(pi => pi.ProductImageMappings);
            }
        }

        public ProductImage GetProductImageById(int productImageId)
        {
            return _storeDbContext.ProductImages.FirstOrDefault(pi => pi.ProductImageId == productImageId);
        }
    }
}
