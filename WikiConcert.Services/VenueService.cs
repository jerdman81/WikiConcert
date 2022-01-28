using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiConcert.Data;
using WikiConcert.Data.Enums;
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
                    Name = model.Name,
                    Address = model.Address,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    Capacity = model.Capacity,
                    AltName = model.AltName,
                    IsOperating = model.IsOperating,
                    CreatedUtc = DateTimeOffset.UtcNow
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
                                Name = e.Name,
                                Address = e.Address,
                                City = e.City,
                                State = e.State.ToString(),
                                ZipCode = e.ZipCode,
                                Capacity = e.Capacity,
                                AltName = e.AltName,
                                OperatingStatus = e.IsOperating,
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
                Venue entity;
                try
                {
                    entity =
                    ctx
                        .Venues
                        .Single(e => e.VenueId == id);
                }
                catch (Exception e)
                {
                    return (null);
                };
                return
                    new VenueDetail
                    {
                        VenueId = entity.VenueId,
                        Name = entity.Name,
                        Address = entity.Address,
                        City = entity.City,
                        State = entity.State.ToString(),
                        ZipCode = entity.ZipCode,
                        Capacity = entity.Capacity,
                        AltName = entity.AltName,
                        OperatingStatus = entity.IsOperating,
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
                Venue entity;
                try
                {
                    entity = ctx
                        .Venues
                        .Single(v => v.VenueId == model.VenueId);

                }
                catch (Exception e)
                {
                    return (false);
                };
                
                entity.Name = model.Name;
                entity.Address = model.Address;
                entity.City = model.City;
                entity.State = model.State;
                entity.ZipCode = model.ZipCode;
                entity.Capacity = model.Capacity;
                entity.AltName = model.AltName;
                entity.IsOperating = model.OperatingStatus;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                
                return ctx.SaveChanges() == 1;
            }
        }

        // Venue Delete
        public bool DeleteVenue(int venueId)
        {
            using(var ctx = new ApplicationDbContext())
            {
                Venue entity;
                try
                {
                    entity =
                    ctx
                        .Venues
                        .Single(v => v.VenueId == venueId);
                }
                catch (Exception e)
                {
                    return (false);
                }

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
                        VenueId = v.VenueId,
                        Name = v.Name,
                        Address = v.Address,
                        City = v.City,
                        State = v.State.ToString(),
                        ZipCode = v.ZipCode,
                        Capacity = v.Capacity,
                        AltName = v.AltName,
                        OperatingStatus = v.IsOperating,
                        CreatedUtc = v.CreatedUtc,
                        ModifiedUtc = v.ModifiedUtc
            });
                return query.ToList();
            }
        }

        // Venue Read by State
        public IEnumerable<VenueDetail> GetVenuesByState(States state)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                ctx
                    .Venues
                    .Where(v => v.State == state)
                    .Select(v => new VenueDetail
                    {
                        VenueId = v.VenueId,
                        Name = v.Name,
                        Address = v.Address,
                        City = v.City,
                        State = v.State.ToString(),
                        ZipCode = v.ZipCode,
                        Capacity = v.Capacity,
                        AltName = v.AltName,
                        OperatingStatus = v.IsOperating,
                        CreatedUtc = v.CreatedUtc,
                        ModifiedUtc = v.ModifiedUtc
                    });
                return query.ToList();
            }
        }

        // Venue Read by City
        public IEnumerable<VenueDetail> GetVenuesByCity(string city)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                ctx
                    .Venues
                    .Where(v => v.City.ToLower() == city.ToLower())
                    .Select(v => new VenueDetail
                    {
                        VenueId = v.VenueId,
                        Name = v.Name,
                        Address = v.Address,
                        City = v.City,
                        State = v.State.ToString(),
                        ZipCode= v.ZipCode,
                        Capacity = v.Capacity,
                        AltName = v.AltName,
                        OperatingStatus = v.IsOperating,
                        CreatedUtc = v.CreatedUtc,
                        ModifiedUtc = v.ModifiedUtc
                    });
                return query.ToList();
            }
        }

        // Venue Read by Name
        public IEnumerable<VenueDetail> GetVenuesByName(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                ctx
                    .Venues
                    .Where(v => v.Name.ToLower() == name.ToLower())
                    .Select(v => new VenueDetail
                    {
                        VenueId = v.VenueId,
                        Name = v.Name,
                        Address = v.Address,
                        City = v.City,
                        State = v.State.ToString(),
                        ZipCode = v.ZipCode,
                        Capacity = v.Capacity,
                        AltName = v.AltName,
                        OperatingStatus = v.IsOperating,
                        CreatedUtc = v.CreatedUtc,
                        ModifiedUtc = v.ModifiedUtc
                    });
                return query.ToList();
            }
        }

        // Venue Read by Capacity
        public IEnumerable<VenueDetail> GetVenuesByCapacity(int capacity, char oper)
        {
            using (var ctx = new ApplicationDbContext())
            {
                if (oper == 'g')
                {
                    var query =
                    ctx
                        .Venues
                        .Where(v => v.Capacity >= capacity)
                        .Select(v => new VenueDetail
                        {
                            VenueId = v.VenueId,
                            Name = v.Name,
                            Address = v.Address,
                            City = v.City,
                            State = v.State.ToString(),
                            ZipCode = v.ZipCode,
                            Capacity = v.Capacity,
                            AltName = v.AltName,
                            OperatingStatus = v.IsOperating,
                            CreatedUtc = v.CreatedUtc,
                            ModifiedUtc = v.ModifiedUtc
                        });
                    return query.ToList();
                }
                else if (oper == 'l')
                {
                    var query =
                   ctx
                       .Venues
                       .Where(v => v.Capacity <= capacity)
                       .Select(v => new VenueDetail
                       {
                           VenueId = v.VenueId,
                           Name = v.Name,
                           Address = v.Address,
                           City = v.City,
                           State = v.State.ToString(),
                           ZipCode = v.ZipCode,
                           Capacity = v.Capacity,
                           AltName = v.AltName,
                           OperatingStatus = v.IsOperating,
                           CreatedUtc = v.CreatedUtc,
                           ModifiedUtc = v.ModifiedUtc
                       });
                    return query.ToList();
                }
                else if (oper == 'e')
                {
                    var query =
                   ctx
                       .Venues
                       .Where(v => v.Capacity == capacity)
                       .Select(v => new VenueDetail
                       {
                           VenueId = v.VenueId,
                           Name = v.Name,
                           Address = v.Address,
                           City = v.City,
                           State = v.State.ToString(),
                           ZipCode = v.ZipCode,
                           Capacity = v.Capacity,
                           AltName = v.AltName,
                           OperatingStatus = v.IsOperating,
                           CreatedUtc = v.CreatedUtc,
                           ModifiedUtc = v.ModifiedUtc
                       });
                    return query.ToList();
                }
                else return null;
                
            }
        }
    }
}
