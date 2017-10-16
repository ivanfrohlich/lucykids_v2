using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucykids_v2.DAL;
using Microsoft.EntityFrameworkCore;

namespace Lucykids_v2.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _storeDbContext;

        public ProductRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public IEnumerable<Product> Products
        {
            get
            {
                return
               _storeDbContext.Products
               .Include(p => p.Category)
               .Include(p => p.Brand)
               .Include(p => p.Size)
               .Include(p =>p.ProductImageMappings) ;
            }
        }

        public Product GetProductById(int productId)
        {
            return _storeDbContext.Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                _storeDbContext.Products.Add(product);
            }
            else
            {
                Product dbEntry = _storeDbContext.Products
                    .FirstOrDefault(p => p.ProductId == product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Brand = product.Brand;
                    dbEntry.Price = product.Price;
                    dbEntry.Size = product.Size;
                    dbEntry.Category = product.Category;
                }
            }
            _storeDbContext.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = _storeDbContext.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (dbEntry != null)
            {
                _storeDbContext.Remove(dbEntry);
                _storeDbContext.SaveChanges();
            }
            return dbEntry;
        }
    }
}
