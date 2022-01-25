using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WikiConcert.Models;
using WikiConcert.Services;

namespace WikiConcert.Controllers
{
    [System.Web.Http.Authorize]
    public class VenueController : ApiController
    {
        private VenueService CreateVenueService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var venueService = new VenueService(userId);
            return venueService;
        }

        public IHttpActionResult Post(VenueCreate venue)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateVenueService();

            if (!service.CreateVenue(venue))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Get()
        {
            VenueService venueService = CreateVenueService();
            var venues = venueService.GetVenues();
            return Ok(venues);
        }

        
    }
}
