using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiConcert.Data;
using WikiConcert.Models;

namespace WikiConcert.Services
{
    public class SongService
    {
        private readonly Guid _userId;

        public SongService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateSong(SongCreate model)
        {
            var entity = new Song
            {
                Name = model.Name,
                Artist = model.Artist,
                ReleaseDate = model.ReleaseDate,
                Lyrics = model.Lyrics,
                Created_At = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Songs.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
