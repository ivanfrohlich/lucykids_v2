using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Models
{
    public class OrderDetail
    {

        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProuctId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
