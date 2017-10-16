using System.ComponentModel.DataAnnotations;

namespace Lucykids_v2.Models
{
    public class ProductImageMapping
    {
        [Key]
        public int ProductImageMappingId { get; set; }
        public int ImageNumber { get; set; }
        public int ProductId { get; set; }
        public int ProductImageId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductImage ProductImage { get; set; }
    }
}