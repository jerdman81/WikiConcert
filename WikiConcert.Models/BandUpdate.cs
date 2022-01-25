using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class BandUpdate
    {
        [Required]
        public int BandId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTimeOffset Modified { get; set; }
    }
}
