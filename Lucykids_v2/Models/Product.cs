using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int? BrandId { get; set; }
        public Brand Brand { get; set; }

        public int? SizeId {get;set;}
        public Size Size { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        //public string ImageUrl { get; set; }
        //public string ImageThumbnailUrl { get; set; }

        public virtual ICollection<ProductImageMapping> ProductImageMappings { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
        
    }
}
