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
    public class BandController : ApiController
    {
        private BandService CreateBandService()
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
            var bandService = new BandService(userId);
            return bandService;
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            BandService bandService = CreateBandService();
            if (bandService == null)
                return Unauthorized();
            var bands = bandService.GetAllBands();
            return Ok(bands);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            BandService bandService = CreateBandService();
            if (bandService == null)
                return Unauthorized();
            BandDetail band;
            try
            {
                band = bandService.GetBandById(id);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Target band id not found.");
            }
            return Ok(band);
        }

        [HttpGet, ActionName("Name")]
        public IHttpActionResult Get(string name)
        {
            BandService bandService = CreateBandService();
            if (bandService == null)
                return Unauthorized();
            var bands = bandService.GetBandByName(name);
            return Ok(bands);
        }
        [HttpGet, ActionName("Genre")]
        public IHttpActionResult GetByGenre(string genre)
        {
            BandService bandService = CreateBandService();
            if (bandService == null)
                return Unauthorized();
            var bands = bandService.GetBandByGenre(genre);
            return Ok(bands);
        }
        [HttpGet, ActionName("IsActive")]
        public IHttpActionResult GetByActive(bool isActive)
        {
            BandService bandService = CreateBandService();
            if (bandService == null)
                return Unauthorized();
            var bands = bandService.GetBandByActive(isActive);
            return Ok(bands);
        }
        /* Redundant
        [HttpGet, ActionName("IsNotActive")]
        public IHttpActionResult GetByNotActive()
        {
            BandService bandService = CreateBandService();
            if (bandService == null)
                return Unauthorized();
            var bands = bandService.GetBandByActive(false);
            return Ok(bands);
        }
        */
        [HttpPost]
        public IHttpActionResult CreateBand(BandCreate band)
        {
            if (band == null)
                return BadRequest("Request body cannot be empty.");
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var bandService = CreateBandService();
            if (bandService == null)
                return Unauthorized();

            if (bandService.CreateBand(band))
                return Ok($"Successfully added {band.Name}");

            return InternalServerError();
        }
        [HttpPut]
        public IHttpActionResult UpdateBand(BandUpdate band)
        {
            if (band == null)
                return BadRequest("Request body cannot be empty.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bandService = CreateBandService();
            if (bandService == null)
                return Unauthorized();
            try
            {
                if (bandService.UpdateBand(band))
                    return Ok($"Successfully updated {band.Name}");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Target band not found.");
            }

            return InternalServerError();
        }
        [HttpDelete]
        public IHttpActionResult BandDelete(int id)
        {
            var bandService = CreateBandService();
            if (bandService == null)
                return Unauthorized();

            try
            {
                if (bandService.DeleteBand(id))
                    return Ok("Successfully deleted band.");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Target band not found.");
            }

            return InternalServerError();
        }
    }
}
