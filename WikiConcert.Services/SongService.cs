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

        public IEnumerable<SongListItem> GetAllSongs()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Songs.Select(s => new SongListItem
                {
                    SongId = s.SongId,
                    Name = s.Name,
                    Artist = s.Artist,
                    ReleaseDate = s.ReleaseDate
                });

                return query.ToList();
            }
        }

        public SongDetail GetSongById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Songs.Single(s => s.SongId == id);
                return new SongDetail
                {
                    SongId = query.SongId,
                    Name = query.Name,
                    Artist = query.Artist,
                    ReleaseDate = query.ReleaseDate,
                    Lyrics = query.Lyrics,
                    Created = query.Created_At,
                    Modified = query.Modified_At
                };
            }
        }

        public IEnumerable<SongListItem> GetSongByName(string name)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Songs.Where(s => s.Name == name).Select(s => new SongListItem
                {
                    SongId = s.SongId,
                    Name = s.Name,
                    Artist = s.Artist,
                    ReleaseDate = s.ReleaseDate
                });

                return query.ToList();
            }
        }

        public IEnumerable<SongListItem> GetSongByArtist(string artist)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Songs.Where(s => s.Artist == artist).Select(s => new SongListItem
                {
                    SongId = s.SongId,
                    Name = s.Name,
                    Artist = s.Artist,
                    ReleaseDate = s.ReleaseDate
                });

                return query.ToList();
            }
        }

        public IEnumerable<SongLyricItem> GetSongByLyric(string lyric)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Songs.Where(s => s.Lyrics.Contains(lyric)).Select(s => new SongLyricItem
                {
                    SongId = s.SongId,
                    Name = s.Name,
                    Artist = s.Artist,
                    ReleaseDate = s.ReleaseDate,
                    Lyrics = s.Lyrics
                });

                return query.ToList();
            }
        }

        public bool UpdateSong(SongUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Songs.Single(s => s.SongId == model.SongId);

                entity.Name = model.Name;
                entity.Artist = model.Artist;
                entity.ReleaseDate = model.ReleaseDate;
                entity.Lyrics = model.Lyrics;
                entity.Modified_At = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteSong(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Songs.Single(s => s.SongId == id);

                ctx.Songs.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
