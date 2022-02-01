using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class ConcertEdit
    {
        [Required]
        public int ConcertId { get; set; }
        [Required]
        public int BandId { get; set; }
        [Required]
        public string ConcertName { get; set; }
        [Required]
        public DateTimeOffset ConcertDate { get; set; }
        [Required]
        public int VenueId { get; set; }
        //public int SetlistId { get; set; }
    }
}
