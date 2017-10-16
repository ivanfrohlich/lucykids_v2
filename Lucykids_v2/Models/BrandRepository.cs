using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucykids_v2.DAL;

namespace Lucykids_v2.Models
{
    public class BrandRepository : IBrandRepository
    {
        private readonly StoreDbContext _storeDbContext;

        public BrandRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public IEnumerable<Brand> Brands => _storeDbContext.Brands;
    }
}
