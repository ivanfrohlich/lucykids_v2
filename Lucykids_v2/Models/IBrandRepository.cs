using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Models
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> Brands { get; }
    }
}
