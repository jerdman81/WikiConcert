using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Data
{
    public class Band
    {
        [Key]
        public int BandId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public bool Active { get; set; }
        public DateTimeOffset Created_At { get; set; }
        public DateTimeOffset? Modified_At { get; set; }
    }
}
