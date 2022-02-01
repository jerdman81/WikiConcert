using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class AttendanceListItem
    {
        public int AttendanceId { get; set; }
        public int ConcertId { get; set; }
        public string ConcertName { get; set; }
    }
}
