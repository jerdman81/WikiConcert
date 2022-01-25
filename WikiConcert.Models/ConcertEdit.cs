using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class ConcertEdit
    {
        public int ConcertId { get; set; }
        public int BandId { get; set; }
        public string ConcertName { get; set; }
        public DateTime ConcertDate { get; set; }
        public int VenueId { get; set; }
        public int SetlistId { get; set; }
    }
}
