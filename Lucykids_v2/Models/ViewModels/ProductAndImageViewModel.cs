using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Models.ViewModels
{
    public class ProductAndImageViewModel
    {
        public Product Product { get; set; }
       // public IEnumerable<ProductImageMapping> ProductImageMappings { get; set; }
        public ProductListViewModel ProductListViewModel { get; set; }
    }
}
