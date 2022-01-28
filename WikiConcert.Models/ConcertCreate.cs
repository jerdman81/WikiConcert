using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class ConcertCreate
    {      
      
        [Required]
        public string ConcertName { get; set; }
        [Required]
        public int BandId { get; set; }
        [Required]
        public DateTime ConcertDate { get; set; }
        [Required]
        public int VenueId { get; set; }        
        //public int SetlistId { get; set; }
        
    }
}
