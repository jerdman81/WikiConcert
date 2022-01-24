using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class Concert
    {
        public int Id { get; set; }
        public int bandId { get; set; }
        public DateTime date { get; set; }
        public int venueId { get; set; }
        public int setListId { get; set; }
    }
}
