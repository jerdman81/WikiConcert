using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Data
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        [Required, ForeignKey (nameof(Concert))]
        public int ConcertId { get; set; }
        public int GuestId { get; set; }

        public virtual Concert Concert { get; set; }
    }
}
