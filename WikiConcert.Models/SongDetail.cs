using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class SongDetail
    {
        public int SongId { get; set; }

        public string Name { get; set; }

        public string Artist { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }
        public string Lyrics { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
    }
}
