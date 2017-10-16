using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Models
{
    interface IProductImageRepository
    {
        IEnumerable<ProductImage> ProductImages { get; }
        ProductImage GetProductImageById(int productImageId);
    }
}
