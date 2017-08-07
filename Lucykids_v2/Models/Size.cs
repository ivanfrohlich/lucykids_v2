using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lucykids_v2.Models
{
    public class Size
    {
        public int SizeId { get; set; }
        [Required(ErrorMessage = "The category name cannot be blank")]
        //[StringLength(20, MinimumLength = 4, ErrorMessage = "Please enter a size between 9 and 14 characters in length  e.g; 5-6 years")]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Please enter a size  e.g; 5-6 years")]
        [Display(Name = "Size Name")]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}