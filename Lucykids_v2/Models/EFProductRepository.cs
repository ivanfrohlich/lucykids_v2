using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucykids_v2.DAL;

namespace Lucykids_v2.Models
{
    public class EFProductRepository: IProductRepository
    {
        private StoreDbContext context;

        public EFProductRepository(StoreDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Product> Products => context.Products;
    }
}
