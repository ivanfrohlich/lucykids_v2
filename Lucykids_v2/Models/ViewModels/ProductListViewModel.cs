using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lucykids_v2.Models;
using PagedList;
using PagedList.Core;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Lucykids_v2.Models.ViewModels
{
    public class ProductListViewModel
    {
        public int ProductListViewModelId { get; set; }
        public IQueryable<Product> Products { get; set; }

        public string CurrentCategory { get; set; }
        public string CurrentBrand { get; set; }
        public string CurrentSize { get; set; }
        public string Size { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
      
        public virtual IQueryable<ProductImage> ProductImages { get; set; }
        public virtual IQueryable<ProductImageMapping> ProductImageMappings { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
