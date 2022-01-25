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
    }
}
