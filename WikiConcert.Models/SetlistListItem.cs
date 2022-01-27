using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class SetlistListItem
    {
        public List<int> SetlistIds { get; set; }
        public string ConcertName { get; set; }
        public int SongCount { get; set; }
    }
}
