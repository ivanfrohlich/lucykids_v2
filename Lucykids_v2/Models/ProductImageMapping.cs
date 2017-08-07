﻿namespace Lucykids_v2.Models
{
    public class ProductImageMapping
    {
        public int ProductImageMappingId { get; set; }
        public int ImageNumber { get; set; }
        public int ProductID { get; set; }
        public int ProductImageID { get; set; }

        public virtual Product Product { get; set; }
        public virtual ProductImage ProductImage { get; set; }
    }
}