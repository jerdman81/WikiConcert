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
    public class ConcertController : ApiController
    {
        private ConcertService CreateConcertService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var concertService = new ConcertService(userId);
            return concertService;
        }

        public IHttpActionResult Post(ConcertCreate concert)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateConcertService();

            if (!service.CreateConcert(concert))
                return InternalServerError();

            return Ok();
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            ConcertService concertService = CreateConcertService();
            var concerts = concertService.GetConcerts();
            return Ok(concerts);
        }
        [HttpGet]
        public IHttpActionResult GetByConcertId(int id)
        {
            ConcertService concertService = CreateConcertService();
            var concerts = concertService.GetConcerts();
            return Ok(concerts);
        }
        [HttpGet]
        public IHttpActionResult GetByVenueId(string venueId)
        {
            ConcertService concertService = CreateConcertService();
            var concerts = concertService.GetConcerts();
            return Ok(concerts);
        }
        [HttpGet]
        public IHttpActionResult GetByDate(DateTime ConcertDate)
        {
            ConcertService concertService = CreateConcertService();
            var concerts = concertService.GetConcerts();
            return Ok(concerts);
        }
        [HttpGet, ActionName("Band")]
        public IHttpActionResult GetByBand(int id)
        {
            ConcertService concertService = CreateConcertService();
            var concerts = concertService.GetConcerts();
            return Ok(concerts);
        }
        [HttpGet]
        public IHttpActionResult Put(ConcertEdit concert)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateConcertService();

            if (!service.EditConcert(concert))
                return InternalServerError();

            return Ok();
        }
        
        [HttpGet]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateConcertService();

            if (!service.DeleteConcert(id))
                return InternalServerError();

            return Ok();
        }
    }
}
