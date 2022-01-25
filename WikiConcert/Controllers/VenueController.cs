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

        [HttpPost]
        public IHttpActionResult Post(VenueCreate venue)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateVenueService();

            if (!service.CreateVenue(venue))
                return InternalServerError();

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            VenueService venueService = CreateVenueService();
            var venues = venueService.GetVenues();
            return Ok(venues);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            VenueService venueService = CreateVenueService();
            var venue = venueService.GetVenueByID(id);
            return Ok(venue);
        }

        [HttpPut]
        public IHttpActionResult Put(VenueEdit venue)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateVenueService();

            if (!service.EditVenue(venue))
                return InternalServerError();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateVenueService();

            if (!service.DeleteVenue(id))
                return InternalServerError();

            return Ok();
        }

        [HttpGet, ActionName("isActive")]
        public IHttpActionResult GetOperationalVenues()
        {
            VenueService venueService = CreateVenueService();
            var venue = venueService.GetVenuesByOperatingStatus(true);
            return Ok(venue);
        }

        [HttpGet, ActionName("notActive")]
        public IHttpActionResult GetNonOperationalVenues()
        {
            VenueService venueService = CreateVenueService();
            var venue = venueService.GetVenuesByOperatingStatus(false);
            return Ok(venue);
        }

        [HttpGet, ActionName("State")]
        public IHttpActionResult GetByState(string state)
        {
            VenueService venueService = CreateVenueService();
            var venue = venueService.GetVenuesByState(state);
            return Ok(venue);
        }

        [HttpGet, ActionName("City")]
        public IHttpActionResult GetByCity(string city)
        {
            VenueService venueService = CreateVenueService();
            var venue = venueService.GetVenuesByCity(city);
            return Ok(venue);
        }

        [HttpGet, ActionName("CapacityGreater")]
        public IHttpActionResult GetByCapacityGreaterThan(int capacity, char oper)
        {
            VenueService venueService = CreateVenueService();
            var venue = venueService.GetVenuesByCapacity(capacity, 'g');
            return Ok(venue);
        }

        [HttpGet, ActionName("CapacityLess")]
        public IHttpActionResult GetByCapacityLessThan(int capacity, char oper)
        {
            VenueService venueService = CreateVenueService();
            var venue = venueService.GetVenuesByCapacity(capacity, 'l');
            return Ok(venue);
        }

        [HttpGet, ActionName("CapacityEqual")]
        public IHttpActionResult GetByCapacityEqualTo(int capacity, char oper)
        {
            VenueService venueService = CreateVenueService();
            var venue = venueService.GetVenuesByCapacity(capacity, 'e');
            return Ok(venue);
        }
    }
}
