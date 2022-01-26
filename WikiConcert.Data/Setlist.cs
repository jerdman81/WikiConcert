using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Data
{
    public class Setlist
    {
        [Key]
        public int SetlistId { get; set; }
        [ForeignKey(nameof(Song))]
        public List<int> SongIds { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        [Required]
        public DateTimeOffset ModifiedUtc { get; set; }

        public virtual List<Song> Songs { get; set; }
    }
}
