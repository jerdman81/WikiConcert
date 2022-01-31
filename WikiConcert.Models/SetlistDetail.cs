using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class SetlistDetail
    {
        public int ConcertId { get; set; }
        public string ConcertName { get; set; }
        public List<int> SetlistItemIds { get; set; }
        public List<int> SongIds { get; set; }
        public List<string> SongsNames { get; set; }
    }
}
