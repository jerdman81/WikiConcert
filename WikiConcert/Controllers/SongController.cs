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
    public class SongController : ApiController
    {
        private SongService CreateSongService()
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
            var songService = new SongService(userId);
            return songService;
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] SongCreate song)
        {
            if (song is null)
                return BadRequest("Your request body cannot be empty.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSongService();
            if (service == null)
                return Unauthorized();

            if (!service.CreateSong(song))
                return InternalServerError();

            return Ok($"Successfully added song {song.Name}");
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            SongService songService = CreateSongService();
            if (songService == null)
                return Unauthorized();
            var songs = songService.GetAllSongs();
            return Ok(songs);
        }

        [HttpGet]
        public IHttpActionResult GetSongById([FromUri]int id)
        {
            SongService songService = CreateSongService();
            if (songService == null)
                return Unauthorized();
            SongDetail song;
            try
            {
                song = songService.GetSongById(id);
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Target song ID not found.");
            }
            return Ok(song);
        }

        [HttpGet]
        public IHttpActionResult GetSongByName([FromUri]string name)
        {
            SongService songService = CreateSongService();
            if (songService == null)
                return Unauthorized();
            var songs = songService.GetSongByName(name);
            return Ok(songs);
        }

        [HttpGet]
        public IHttpActionResult GetSongByArtist([FromUri]string name)
        {
            SongService songService = CreateSongService();
            if (songService == null)
                return Unauthorized();
            var songs = songService.GetSongByArtist(name);
            return Ok(songs);
        }

        [HttpGet]
        public IHttpActionResult GetSongByLyrics([FromUri]string lyric)
        {
            SongService songService = CreateSongService();
            if (songService == null)
                return Unauthorized();
            var songs = songService.GetSongByLyrics(lyric);
            return Ok(songs);
        }

        [HttpPut]
        public IHttpActionResult UpdateSong([FromBody]SongUpdate song)
        {
            if (song == null)
                return BadRequest("Your model cannot be empty.");
            // I don't think this is necesary ~PT
            /*
            if (id != song.SongId)
                return BadRequest("Ids do not match.");
            */

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            SongService service = CreateSongService();
            if (service == null)
                return Unauthorized();
            try
            {
                if (service.UpdateSong(song))
                    return Ok($"Successfully updated {song.Name}.");
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Target song not found.");
            }

            return InternalServerError();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateSongService();
            if (service == null)
                return Unauthorized();

            try
            {
                if (!service.DeleteSong(id))
                    return InternalServerError();
            }
            catch (InvalidOperationException)
            {
                return BadRequest("Target song not found.");
            }

            return Ok($"Successfully deleted song {id}.");




        }
    }
}
