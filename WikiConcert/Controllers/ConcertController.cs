﻿using Microsoft.AspNet.Identity;
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
            var concerts = concertService.GetConcertById(id);
            return Ok(concerts);
        }
        [HttpGet]
        public IHttpActionResult GetByVenueId(int venueId)
        {
            ConcertService concertService = CreateConcertService();
            var concerts = concertService.GetConcertByVenueId(venueId);
            return Ok(concerts);
        }
        [HttpGet]
        public IHttpActionResult GetByDate(DateTimeOffset concertDate)
        {
            ConcertService concertService = CreateConcertService();
            var concerts = concertService.GetConcertByDate(concertDate);
            return Ok(concerts);
        }
        [HttpGet, ActionName("Band")]
        public IHttpActionResult GetByBand(int id)
        {
            ConcertService concertService = CreateConcertService();
            var concerts = concertService.GetConcertByBandId(id);
            return Ok(concerts);
        }
        /*
        [HttpGet, ActionName("GetBySong")]
        public IHttpActionResult GetBySong(int id)
        {
            ConcertService concertService = CreateConcertService();
            var concerts = concertService.GetConcertBySong(id);
            return Ok(concerts);
        }
        */
        [HttpPut]
        public IHttpActionResult Put(ConcertEdit concert)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateConcertService();

            if (!service.EditConcert(concert))
                return InternalServerError();

            return Ok();
        }
        
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateConcertService();

            if (!service.DeleteConcert(id))
                return InternalServerError();

            return Ok();
        }
    }
}
