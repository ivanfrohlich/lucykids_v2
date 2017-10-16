using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Models.ViewModels
{
    //PagingInfo class is  passing data between controller and the view
    public class PagingInfo
    {
        //givin error that primary key must be declared?
        //public int Id { get; set; }
        //the total number of products in the reposirory
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
    }
}
