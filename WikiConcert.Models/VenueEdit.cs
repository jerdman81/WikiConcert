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
        public string VenueName { get; set; }
        public string VenueAddress { get; set; }
        public string VenueCity { get; set; }
        public string VenueState { get; set; }
        public int VenueCapacity { get; set; }
        public string VenueAltName { get; set; }
        public bool VenueOperatingStatus { get; set; }
    }
}
