using Lucykids_v2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Models
{
    public class SizeRepository : ISizeRepository
    {

        private readonly StoreDbContext _storeDbContext;

        public SizeRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public IEnumerable<Size> Sizes => _storeDbContext.Sizes;
    }
}
