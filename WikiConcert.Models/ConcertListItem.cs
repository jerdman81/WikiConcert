using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class ConcertListItem
    {
        public int ConcertId { get; set; }
        public int BandId { get; set; }
        public string ConcertName { get; set;}
        public DateTimeOffset? ConcertDate { get; set; }
        public int VenueId { get; set; }
    }
}
