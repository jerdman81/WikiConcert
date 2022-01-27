using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class BandDetail
    {
        public int BandId { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public bool IsActive { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Modified { get; set; }
    }
}
