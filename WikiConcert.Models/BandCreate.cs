using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class BandCreate
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public bool IsActive { get; set; }
    }
}
