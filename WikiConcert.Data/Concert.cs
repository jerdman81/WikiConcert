using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Data
{
    public class Concert
    {
        [Key]
        public int ConcertId { get; set; }
        [Required]
        public int BandId { get; set; }
        [Required]
        public DateTime ConcertDate { get; set; }
        [Required]
        public int VenueId { get; set; }
        [Required]
        public int SetlistId { get; set; }
        [Required]
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
