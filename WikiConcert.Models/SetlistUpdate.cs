using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class SetlistUpdate
    {
        [Required]
        public int ConcertId { get; set; }
        [Required]
        public int SongId { get; set; }
    }
}
