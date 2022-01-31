using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class SetlistListItem
    {
        public List<int> SetlistItemIds { get; set; }
        public int ConcertId { get; set; }
        public string ConcertName { get; set; }
        public int SongCount { get; set; }
    }
}
