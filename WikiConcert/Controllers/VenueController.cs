using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WikiConcert.Data;
using WikiConcert.Data.Enums;
using WikiConcert.Models;
using WikiConcert.Services;

namespace WikiConcert.Controllers
{
    [System.Web.Http.Authorize]
    public class VenueController : ApiController
    {
        private VenueService CreateVenueService()
        {
            Guid userId;
            try
            {
                userId = Guid.Parse(User.Identity.GetUserId());
            }
            catch (System.ArgumentNullException anex)
            {
                return null;
            }
            var venueService = new VenueService(userId);
            return venueService;
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] VenueCreate venue)
        {
            if (venue is null)
                return BadRequest("Your request body cannot be empty.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateVenueService();
            if (service == null)
                return Unauthorized();

            if (!service.CreateVenue(venue))
                return InternalServerError();

            return Ok($"You created a Venue named {venue.Name} successfully!");
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            VenueService venueService = CreateVenueService();
            if (venueService == null)
                return Unauthorized();
            var venues = venueService.GetVenues();
            return Ok(venues);
        }

        [HttpGet]
        public IHttpActionResult Get([FromUri] int id)
        {
            VenueService venueService = CreateVenueService();
            if (venueService == null)
                return Unauthorized();
            VenueDetail venue;
            try
            {
            venue = venueService.GetVenueByID(id);

            }
            catch (InvalidOperationException)
            {
                return BadRequest("Target venue ID not found.");
            }

            if (venue != null)
            {
                return Ok(venue);
            }
            return InternalServerError();
        }

        [HttpPut]
        public IHttpActionResult Put(/*[FromUri] int id, */[FromBody] VenueEdit venue)
        {
            if (venue is null)
                return BadRequest("Your model cannot be empty.");
            /*  I don't think this check is necessary. ~PT
            if (id != venue.VenueId)
                return BadRequest("Ids do not match");
            */
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateVenueService();
            if (service == null)
                return Unauthorized();
            try
            {
                if (!service.EditVenue(venue))
                    return InternalServerError();
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Target venue not found.");
            }

            return Ok($"Successfully updated Venue {venue.VenueId}.");
        }

        [HttpDelete]
        public IHttpActionResult Delete([FromUri] int id)
        {
            var service = CreateVenueService();
            if (service == null)
                return Unauthorized();
            try
            {
                if (!service.DeleteVenue(id))
                    return InternalServerError();
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Target venue not found.");
            }

            return Ok($"Successfully deleted venue {id}.");
        }

        [HttpGet, ActionName("IsActive")]
        public IHttpActionResult GetOperationalVenues(bool isActive)
        {
            VenueService venueService = CreateVenueService();
            if (venueService == null)
                return Unauthorized();
            var venue = venueService.GetVenuesByOperatingStatus(isActive);
            return Ok(venue);
        }
        /*  I have found this to be redundant. ~PT
        [HttpGet, ActionName("notActive")]
        public IHttpActionResult GetNonOperationalVenues()
        {
            VenueService venueService = CreateVenueService();
            if (venueService == null)
                return Unauthorized();
            var venue = venueService.GetVenuesByOperatingStatus(false);
            return Ok(venue);
        }
        */
        [HttpGet, ActionName("State")]
        public IHttpActionResult GetByState([FromUri] States state)
        {
            VenueService venueService = CreateVenueService();
            if (venueService == null)
                return Unauthorized();
            var venue = venueService.GetVenuesByState(state);
            return Ok(venue);
        }

        [HttpGet, ActionName("City")]
        public IHttpActionResult GetByCity([FromUri] string city)
        {
            if (city is null)
                return BadRequest("Your paramaters cannot be empty.");

            VenueService venueService = CreateVenueService();
            if (venueService == null)
                return Unauthorized();
            var venue = venueService.GetVenuesByCity(city);
            return Ok(venue);
        }

        [HttpGet, ActionName("Name")]
        public IHttpActionResult GetByName([FromUri] string name)
        {
            if (name is null)
                return BadRequest("Your paramaters cannot be empty.");

            VenueService venueService = CreateVenueService();
            if (venueService == null)
                return Unauthorized();
            var venue = venueService.GetVenuesByName(name);
            return Ok(venue);
        }

        [HttpGet, ActionName("CapacityGreater")]
        public IHttpActionResult GetByCapacityGreaterThan([FromUri] int capacity)
        {
            VenueService venueService = CreateVenueService();
            if (venueService == null)
                return Unauthorized();
            var venue = venueService.GetVenuesByCapacity(capacity, 'g');
            return Ok(venue);
        }

        [HttpGet, ActionName("CapacityLess")]
        public IHttpActionResult GetByCapacityLessThan([FromUri] int capacity)
        {
            VenueService venueService = CreateVenueService();
            if (venueService == null)
                return Unauthorized();
            var venue = venueService.GetVenuesByCapacity(capacity, 'l');
            return Ok(venue);
        }

        [HttpGet, ActionName("CapacityEqual")]
        public IHttpActionResult GetByCapacityEqualTo([FromUri]int capacity)
        {
            VenueService venueService = CreateVenueService();
            if (venueService == null)
                return Unauthorized();
            var venue = venueService.GetVenuesByCapacity(capacity, 'e');
            return Ok(venue);
        }
    }
}
