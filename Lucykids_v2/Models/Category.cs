using System.Collections.Generic;

namespace Lucykids_v2.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}