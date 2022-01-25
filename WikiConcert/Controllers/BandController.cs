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
            var userId = Guid.Parse(User.Identity.GetUserId());
            var bandService = new BandService(userId);
            return bandService;
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            BandService bandService = CreateBandService();
            var bands = bandService.GetAllBands();
            return Ok(bands);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            BandService bandService = CreateBandService();
            var bands = bandService.GetBandById(id);
            return Ok(bands);
        }

        [HttpGet, ActionName("Name")]
        public IHttpActionResult Get(string name)
        {
            BandService bandService = CreateBandService();
            var bands = bandService.GetBandByName(name);
            return Ok(bands);
        }
        [HttpGet, ActionName("Genre")]
        public IHttpActionResult GetByGenre(string genre)
        {
            BandService bandService = CreateBandService();
            var bands = bandService.GetBandByGenre(genre);
            return Ok(bands);
        }
        [HttpGet, ActionName("IsActive")]
        public IHttpActionResult GetByActive()
        {
            BandService bandService = CreateBandService();
            var bands = bandService.GetBandByActive(true);
            return Ok(bands);
        }
        [HttpGet, ActionName("IsNotActive")]
        public IHttpActionResult GetByNotActive()
        {
            BandService bandService = CreateBandService();
            var bands = bandService.GetBandByActive(false);
            return Ok(bands);
        }
        [HttpPost]
        public IHttpActionResult CreateBand(BandCreate band)
        {
            if (band == null)
                return BadRequest("Request body cannot be empty.");
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var bandService = CreateBandService();

            if(bandService.CreateBand(band))
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

            if (bandService.UpdateBand(band))
                return Ok($"Successfully updated {band.Name}");

            return InternalServerError();
        }
        [HttpDelete]
        public IHttpActionResult BandDelete(int id)
        {
            var bandService = CreateBandService();

            if (bandService.DeleteBand(id))
                return Ok("Successfully deleted band.");

            return InternalServerError();
        }
    }
}
