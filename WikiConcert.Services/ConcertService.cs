﻿using System;
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
        private readonly Guid userId;

        public ConcertService(Guid userId)
        {
            userId = userId;
        }

        public bool CreateConcert(ConcertCreate model)
        {
            var entity =
                new Concert()
                {
                    ConcertId = model.ConcertId,
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

        public IEnumerable<ConcertDetail> GetConcerts()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Concerts
                    .Select(
                        e =>
                        new ConcertDetail
                        {
                            ConcertId = e.ConcertId,
                            BandId = e.BandId,
                            ConcertDate = e.ConcertDate,
                            VenueId = e.VenueId,
                            SetlistId = e.SetlistId,
                            CreatedUtc = e.CreatedUtc,
                            ModifiedUtc = e.ModifiedUtc
                        });
                return query.ToArray();
            }
        }

        public ConcertDetail GetConcertById(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity = 
                    ctx
                    .Concerts
                    .Single(e => e.ConcertId == id);
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

        public bool EditConcert(ConcertEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Concerts
                    .Single(C => C.ConcertId == model.ConcertId);

                entity.ConcertId = model.ConcertId;
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
                var entity = 
                    ctx
                    .Concerts
                    .Single(C => C.ConcertId == concertId);

                ctx.Concerts.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
