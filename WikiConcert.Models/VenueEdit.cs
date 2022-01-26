using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class VenueEdit
    {
        public int VenueId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public int Capacity { get; set; }
        public string AltName { get; set; }
        public bool OperatingStatus { get; set; }
    }
}
