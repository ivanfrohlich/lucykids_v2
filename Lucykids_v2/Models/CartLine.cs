using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Models
{
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        // CartId is used to link CartId with shopping cart id[CartId] in Cart model class
        public string CartId { get; set; }
        //public ProductImage ProductImage { get; set; }
        public virtual ICollection<ProductImageMapping> ProductImageMappings { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
