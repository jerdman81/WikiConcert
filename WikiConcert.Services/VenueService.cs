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

    }
}
