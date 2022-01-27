using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class SetlistListItem
    {
        public int SetlistId { get; set; }
        public List<string> Songs { get; set; }
    }
}
