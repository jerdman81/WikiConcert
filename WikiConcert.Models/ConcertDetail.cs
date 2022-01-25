using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class ConcertDetail
    {
        public int ConcertId { get; set; }
        public string ConcertName { get; set;}
        public DateTime ConcertDate { get; set; }
        public int VenueId { get; set; }
        public int SetlistId { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified")]
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
