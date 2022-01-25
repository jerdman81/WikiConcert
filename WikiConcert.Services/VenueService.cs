using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiConcert.Data;
using WikiConcert.Models;

namespace WikiConcert.Services
{
    public class VenueService
    {
        private readonly Guid _userId;

        public VenueService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateVenue(VenueCreate model)
        {
            var entity =
                new Venue()
                {
                    Name = model.VenueName,
                    Address = model.VenueAddress,
                    City = model.VenueCity,
                    State = model.VenueState,
                    Capacity = model.VenueCapacity,
                    AltName = model.VenueAltName,
                    IsOperating = model.VenueIsOperating
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Venues.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        // Get All Venues
        public IEnumerable<VenueDetail> GetVenues()
        {
            using(var ctx = new ApplicationDbContext())
            {
                var query =
                ctx
                    .Venues
                    .Select(
                        e =>
                            new VenueDetail
                            {
                                VenueId = e.VenueId,
                                VenueName = e.Name,
                                VenueAddress = e.Address,
                                VenueCity = e.City,
                                VenueState = e.State,
                                VenueCapacity = e.Capacity,
                                VenueAltName = e.AltName,
                                VenueOperatingStatus = e.IsOperating,
                                CreatedUtc = e.CreatedUtc,
                                ModifiedUtc = e.ModifiedUtc
                            });
                return query.ToArray();
            }
        }

        // Get Venue By ID
        public VenueDetail GetVenueByID(int id)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Venues
                        .Single(e => e.VenueId == id);
                return
                    new VenueDetail
                    {
                        VenueId = entity.VenueId,
                        VenueName = entity.Name,
                        VenueAddress = entity.Address,
                        VenueCity = entity.City,
                        VenueState = entity.State,
                        VenueCapacity = entity.Capacity,
                        VenueAltName = entity.AltName,
                        VenueOperatingStatus = entity.IsOperating,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        // Venue Update
        public bool EditVenue(VenueEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Venues
                        .Single(v => v.VenueId == model.VenueId);

                entity.Name = model.VenueName;
                entity.Address = model.VenueAddress;
                entity.City = model.VenueCity;
                entity.State = model.VenueState;
                entity.Capacity = model.VenueCapacity;
                entity.AltName = model.VenueAltName;
                entity.IsOperating = model.VenueOperatingStatus;

                return ctx.SaveChanges() == 1;
            }
        }

        // Venue Delete
        public bool DeleteVenue(int venueId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Venues
                        .Single(v => v.VenueId == venueId);

                ctx.Venues.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        // Venue Read by Operating Status
        public IEnumerable<VenueDetail> GetVenuesByOperatingStatus(bool operatingStatus)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                ctx
                    .Venues
                    .Where(v => v.IsOperating == operatingStatus)
                    .Select(v => new VenueDetail
                    {
                        VenueName = v.Name,
                        VenueAddress = v.Address,
                        VenueCity = v.City,
                        VenueState = v.State,
                        VenueCapacity = v.Capacity,
                        VenueAltName = v.AltName,
                        VenueOperatingStatus = v.IsOperating
            });
                return query.ToList();
            }
        }
    }
}
