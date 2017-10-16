using Lucykids_v2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDbContext _storeDbContext;
        public CategoryRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }

        public IEnumerable<Category> Categories => _storeDbContext.Categories;
    }
}
