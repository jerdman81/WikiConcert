using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class VenueCreate
    {
        [Required]
        public string VenueName { get; set; }
        [Required]
        public string VenueAddress { get; set; }
        [Required]
        public string VenueCity { get; set; }
        [Required]
        [MaxLength(2, ErrorMessage ="Please enter the 2 character state abbreviation."), MinLength(2, ErrorMessage ="Please enter the 2 character state abbreviation.")]
        public string VenueState { get; set; }
        [Required]
        public int VenueZipCode { get; set; }
        public int VenueCapacity { get; set; }
        public string VenueAltName { get; set; }
        [Required]
        public bool VenueIsOperating { get; set; }
    }
}
