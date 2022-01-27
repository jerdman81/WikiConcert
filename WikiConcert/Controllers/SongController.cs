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
            var userId = Guid.Parse(User.Identity.GetUserId());
            var songService = new SongService(userId);
            return songService;
        }

        [HttpPost]
        public IHttpActionResult Post(SongCreate song)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateSongService();

            if (!service.CreateSong(song))
                return InternalServerError();

            return Ok($"Successfully added song {song.Name}");
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            SongService songService = CreateSongService();
            var songs = songService.GetAllSongs();
            return Ok(songs);
        }

        [HttpGet]
        public IHttpActionResult GetSongById(int id)
        {
            SongService songService = CreateSongService();
            var songs = songService.GetSongById(id);
            return Ok(songs);
        }

        [HttpGet]
        public IHttpActionResult GetSongByName(string name)
        {
            SongService songService = CreateSongService();
            var songs = songService.GetSongByName(name);
            return Ok(songs);
        }

        [HttpGet]
        public IHttpActionResult GetSongByArtist(string name)
        {
            SongService songService = CreateSongService();
            var songs = songService.GetSongByArtist(name);
            return Ok(songs);
        }

        [HttpGet]
        public IHttpActionResult GetSongByLyrics(string lyric)
        {
            SongService songService = CreateSongService();
            var songs = songService.GetSongByLyrics(lyric);
            return Ok(songs);
        }

        [HttpPut]
        public IHttpActionResult UpdateSong(SongUpdate song)
        {
            SongService service = CreateSongService();

            if (service.UpdateSong(song))
                return Ok("Successfully updated song.");

            return InternalServerError();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateSongService();

            if (!service.DeleteSong(id))
                return InternalServerError();

            return Ok();




        }
    }
}
