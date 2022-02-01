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
            Guid userId;
            try
            {
                userId = Guid.Parse(User.Identity.GetUserId());
            }
            catch (System.ArgumentNullException anex)
            {
                return null;
            }
            var setlistService = new SetlistService(userId);
            return setlistService;
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            SetlistService service = CreateSetlistService();
            if (service == null)
                return Unauthorized();
            var setlists = service.GetAllSetLists();
            return Ok(setlists);
        }
        //Searches by Concert ID
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            SetlistService service = CreateSetlistService();
            if (service == null)
                return Unauthorized();
            SetlistDetail setlist;
            try
            {
                setlist = service.GetSetlistByConcertId(id);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Target id not found.");
            }
            return Ok(setlist);
        }
        [HttpPost]
        public IHttpActionResult Post(SetlistCreate setlist)
        {
            if(setlist == null)
                return BadRequest("Request body cannot be empty.");
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSetlistService();
            if (service == null)
                return Unauthorized();
            try
            {
                if (service.AddSongToSetlist(setlist))
                    return Ok($"Successfully added setlist.");
            }
            catch(Exception)
            {
                return BadRequest("One or more ID's are invalid.");
            }

            return InternalServerError();
        }
        /*
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
        */
        [HttpDelete]
        public IHttpActionResult SetlistDelete(int id)
        {
            var service = CreateSetlistService();
            if (service == null)
                return Unauthorized();
            try
            {
                if (service.DeleteSetlist(id))
                    return Ok("Successfully deleted setlist.");
            }
            catch(InvalidOperationException)
            {
                return BadRequest("Target setlist not found.");
            }

            return InternalServerError();
        }

        [HttpDelete, ActionName("SetlistItemDelete")]
        public IHttpActionResult SetlistItemDelete(int id)
        {
            var service = CreateSetlistService();
            if (service == null)
                return Unauthorized();

            try
            {
                if (service.DeleteSetlistItem(id))
                    return Ok("Successfully deleted setlist.");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Target setlist item not found.");
            }

            return InternalServerError();
        }
    }
}
