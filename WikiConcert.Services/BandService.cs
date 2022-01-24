using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiConcert.Data;
using WikiConcert.Models;

namespace WikiConcert.Services
{
    public class BandService
    {
        private readonly Guid _userId;

        public BandService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateBand(BandCreate model)
        {
            var entity = new Band()
            {
                Name = model.Name,
                Genre = model.Genre,
                Active = model.IsActive,
                Created_At = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Bands.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
        /*
        public IEnumerable<BandListItem> GetAllBands()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Bands.Select(b => new BandListItem
                {

                })
            }
        }
        */
    }
}
