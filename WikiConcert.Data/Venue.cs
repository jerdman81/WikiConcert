using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Data
{
    public class Venue
    {
        [Key]
        public int VenueId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [MaxLength(2), MinLength(2)]
        public string State { get; set; }
        public int Capacity { get; set; }        
        public string AltName { get; set; }
        [Required]
        public bool IsOperating { get; set; }
        [Required]
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc  { get; set; }
        public DateTimeOffset ModifiedUtc { get; set; }
    }
}
