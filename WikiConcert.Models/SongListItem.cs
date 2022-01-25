using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class SongListItem
    {
        public int SongId { get; set; }

        public string Name { get; set; }

        public string Artist { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }
    }
}
