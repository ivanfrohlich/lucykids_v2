using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Lucykids_v2.Models
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }
        [Display(Name ="File")]
        public string FileName { get; set; }

        public virtual ICollection<ProductImageMapping> ProductImageMappings { get; set; }
    }
}
