using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiConcert.Data;
using WikiConcert.Models;

namespace WikiConcert.Services
{
    public class SetlistService
    {
        private readonly Guid _userId;

        public SetlistService(Guid userId)
        {
            _userId = userId;
        }

        public bool AddSongToSetlist(SetlistCreate model)
        {
            var entity = new Setlist
            {
                SongId = model.SongId,
                ConcertId = model.ConcertId
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Setlists.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<SetlistListItem> GetAllSetLists()
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<SetlistListItem> setList = new List<SetlistListItem>();
                var query = ctx.Setlists.GroupBy(s => s.Concert.ConcertName);
                foreach (var group in query)
                {
                    setList.Add(new SetlistListItem
                    {
                        SetlistIds = group.Select(s => s.SetlistId).ToList(),
                        ConcertName = group.Key,
                        SongCount = group.Count()
                    }); ; ;
                }

                return setList;
            }
        }

        public SetlistDetail GetSetlistByConcertId(int concertId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                IEnumerable<Setlist> sets;
                try
                {
                    sets = ctx.Setlists.Where(s => s.ConcertId == concertId);
                }
                catch (Exception ex)
                {
                    return null;
                }
                return new SetlistDetail
                {
                    SetlistIds = sets.Select(s => s.SetlistId).ToList(),
                    ConcertId = sets.First().ConcertId,
                    ConcertName = sets.First().Concert.ConcertName,
                    SongIds = sets.Select(s => s.SongId).ToList(),
                    SongsNames = sets.Select(s => s.Song.Name).ToList()
                };
            }
        }
        /*
        public bool UpdateSetlist(SetlistUpdate model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Setlist entity;
                try
                {
                    entity = ctx.Setlists.Single(s => s.SetlistId == model.SetlistId);
                }
                catch (Exception ex)
                {
                    return false;
                }
                entity.SongIds = model.SongIds;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() == 1;
            }
        }
        */
        public bool DeleteSetlist(int setlistId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Setlist entity;
                try
                {
                    entity = ctx.Setlists.Single(s => s.SetlistId == setlistId);
                }
                catch (Exception ex)
                {
                    return false;
                }

                ctx.Setlists.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
