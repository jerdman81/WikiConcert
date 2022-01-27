using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string ConcertName { get; set; }
        [Required]
        [ForeignKey("Band")]
        public int BandId { get; set; }
        [Required]
        public DateTime ConcertDate { get; set; }
        [Required]
        [ForeignKey("Venue")]
        public int VenueId { get; set; }
        [Required]
        [ForeignKey("Setlist")]
        public int SetlistId { get; set; }
        [Required]
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }

        public virtual Setlist Setlist { get; set; }
        public virtual Band Band { get; set; }
        public virtual Venue Venue { get; set; }
    }
}
