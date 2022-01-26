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
    public class SetlistController : ApiController
    {
        private SetlistService CreateSetlistService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var setlistService = new SetlistService(userId);
            return setlistService;
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            SetlistService service = CreateSetlistService();
            var setlists = service.GetAllSetLists();
            return Ok(setlists);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            SetlistService service = CreateSetlistService();
            var setlists = service.GetSetlistById(id);
            return Ok(setlists);
        }
        [HttpPost]
        public IHttpActionResult Post(SetlistCreate setlist)
        {
            if(setlist == null)
                return BadRequest("Request body cannot be empty.");
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSetlistService();

            if (service.CreateSetlist(setlist))
                return Ok($"Successfully added setlist.");

            return InternalServerError();
        }
        [HttpPut]
        public IHttpActionResult UpdateSetlist(SetlistUpdate setlist)
        {
            if (setlist == null)
                return BadRequest("Request body cannot be empty.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSetlistService();

            if (service.UpdateSetlist(setlist))
                return Ok("Successfully updated setlist");

            return InternalServerError();
        }
        [HttpDelete]
        public IHttpActionResult SetlistDelete(int id)
        {
            var service = CreateSetlistService();

            if (service.DeleteSetlist(id))
                return Ok("Successfully deleted setlist.");

            return InternalServerError();
        }
    }
}
