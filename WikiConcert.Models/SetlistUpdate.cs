using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiConcert.Models
{
    public class SetlistUpdate
    {
        [Required]
        public int SetlistId { get; set; }
        [Required]
        public List<int> SongIds { get; set; }
    }
}
