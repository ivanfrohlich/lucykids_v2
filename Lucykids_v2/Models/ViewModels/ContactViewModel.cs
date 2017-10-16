using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Models.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [StringLength(20,MinimumLength =2)]
        public string FirstName { get; set; }
        public string LastName { get; set;}
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Message { get; set; }
    }
}
