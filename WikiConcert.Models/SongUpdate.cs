using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class SongUpdate
    {
        [Required]
        public int SongId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Artist { get; set; }
        [Required]
        public DateTimeOffset ReleaseDate { get; set; }
        public string Lyrics { get; set; }
    }
}
