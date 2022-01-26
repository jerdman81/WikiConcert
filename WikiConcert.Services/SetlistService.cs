using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiConcert.Data;
using WikiConcert.Models;

namespace WikiConcert.Services
{
    public class SetlistService
    {
        private readonly Guid _userId;

        public SetlistService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateSetlist(SetlistCreate model)
        {
            var entity = new Setlist
            {
                SongIds = model.SongIds,
                CreatedUtc = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Setlists.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
