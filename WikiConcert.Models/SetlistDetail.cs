using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class SetlistDetail
    {
        public int SetlistId { get; set; }
        public string ConcertName { get; set; }
        public List<string> SongsNames { get; set; }
    }
}
