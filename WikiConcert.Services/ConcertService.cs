using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiConcert.Data;
using WikiConcert.Models;

namespace WikiConcert.Services
{
    public class ConcertService
    {
        private readonly Guid _userId;

        public ConcertService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateConcert(ConcertCreate model)
        {
            var entity =
                new Concert()
                {
                    ConcertName = model.ConcertName,
                    BandId = model.BandId,
                    VenueId = model.VenueId,
                    SetlistId = model.SetlistId
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Concerts.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<ConcertListItem> GetConcerts()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                ctx
                    .Concerts
                    .Select(
                        e =>
                        new ConcertListItem
                        {
                            ConcertId = e.ConcertId,
                            ConcertName = e.ConcertName,
                            BandId = e.BandId,
                            ConcertDate = e.ConcertDate,
                            VenueId = e.VenueId
                        });
                return query.ToArray();
            }
        }

        public ConcertDetail GetConcertById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                Concert entity;
                try
                {
                    entity =
                    ctx
                        .Concerts
                        .Single(e => e.ConcertId == id);
                }
                catch (Exception ex)
                {
                    return null;
                }
                return
                    new ConcertDetail
                    {
                        ConcertId = entity.ConcertId,
                        BandId = entity.BandId,
                        ConcertDate = entity.ConcertDate,
                        VenueId = entity.VenueId,
                        SetlistId = entity.SetlistId,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };

            }
        }
        public IEnumerable<ConcertListItem> GetConcertByVenueId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Concerts.Where(c => c.VenueId == id)
                    .Select(c => new ConcertListItem
                    {
                        ConcertId = c.ConcertId,
                        ConcertName = c.ConcertName,
                        BandId = c.BandId,
                        ConcertDate = c.ConcertDate,
                        VenueId = c.VenueId
                    });
                return entity.ToList();
            }
        }
        public IEnumerable<ConcertListItem> GetConcertByBandId(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Concerts.Where(c => c.BandId == id)
                    .Select(c => new ConcertListItem
                    {
                        ConcertId = c.ConcertId,
                        ConcertName = c.ConcertName,
                        BandId = c.BandId,
                        ConcertDate = c.ConcertDate,
                        VenueId = c.VenueId
                    });

                return entity.ToList();
            }
        }
        public IEnumerable<ConcertListItem> GetConcertByDate(DateTimeOffset concertDate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Concerts.Where(c => c.ConcertDate == concertDate)
                    .Select(c => new ConcertListItem
                    {
                        ConcertId = c.ConcertId,
                        ConcertName = c.ConcertName,
                        BandId = c.BandId,
                        ConcertDate = c.ConcertDate,
                        VenueId = c.VenueId
                    });

                return entity.ToList();
            }
        }

        public IEnumerable<ConcertDetail> GetConcertBySong(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Concerts
                    .Where(c => c.Setlist.SongIds.Contains(id))
                    .Select(c => new ConcertDetail
                    {
                        ConcertId = c.ConcertId,
                        ConcertName = c.ConcertName,
                        BandId = c.BandId,
                        ConcertDate = c.ConcertDate,
                        VenueId = c.VenueId,
                        SetlistId = c.SetlistId,
                        CreatedUtc = c.CreatedUtc,
                        ModifiedUtc = c.ModifiedUtc
                    });

                return entity.ToList();
            }
        }

        public bool EditConcert(ConcertEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Concert entity;
                try
                {
                    entity =
                        ctx
                        .Concerts
                        .Single(C => C.ConcertId == model.ConcertId);
                }
                catch (Exception ex)
                {
                    return false;
                }

                entity.ConcertId = model.ConcertId;
                entity.ConcertName = model.ConcertName;
                entity.BandId = model.BandId;
                entity.ConcertDate = model.ConcertDate;
                entity.VenueId = model.VenueId;
                entity.SetlistId = model.SetlistId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteConcert(int concertId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Concert entity;
                try
                {
                    entity = 
                        ctx
                        .Concerts
                        .Single(C => C.ConcertId == concertId);
                }
                catch (Exception ex)
                {
                    return false;
                }

                ctx.Concerts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
