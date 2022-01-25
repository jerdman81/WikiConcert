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
        
        public IEnumerable<BandListItem> GetAllBands()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Bands.Select(b => new BandListItem
                {
                    Name = b.Name,
                    Genre = b.Genre,
                    IsActive = b.Active,
                    Created = b.Created_At
                });

                return query.ToList();
            };
        }

        public IEnumerable<BandListItem> GetBandByName(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Bands.Where(b => b.Name == name).Select(b => new BandListItem
                {
                    BandId = b.BandId,
                    Name = b.Name,
                    Genre = b.Genre,
                    IsActive=b.Active,
                    Created=b.Created_At
                });

                return query.ToList();
            }
        }

        public IEnumerable<BandListItem> GetBandByGenre(string genre)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Bands.Where(b => b.Genre == genre).Select(b => new BandListItem
                {
                    BandId = b.BandId,
                    Name = b.Name,
                    Genre = b.Genre,
                    IsActive = b.Active,
                    Created = b.Created_At
                });

                return query.ToList();
            }
        }

        public bool UpdateBand(BandUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Bands.Single(b => b.BandId == model.BandId);

                entity.Name = model.Name;
                entity.Genre = model.Genre;
                entity.Active = model.IsActive;
                entity.Modified_At = model.Modified;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBand(int BandId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Bands.Single(b => b.BandId == BandId);

                ctx.Bands.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
        
    }
}
