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
        public int SetlistItemId { get; set; }
        [ForeignKey(nameof(Song))]
        public int SongId { get; set; }
        [ForeignKey(nameof(Concert))]
        public int ConcertId { get; set; }

        public virtual Song Song { get; set; }
        public virtual Concert Concert { get; set; }
    }
}
