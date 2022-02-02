using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiConcert.Data.Enums;

namespace WikiConcert.Models
{
    public class VenueCreate
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public States State { get; set; }
        [Required]
        public int ZipCode { get; set; }
        public int Capacity { get; set; }
        public string AltName { get; set; }
        [Required]
        public bool IsOperating { get; set; }
    }
}
