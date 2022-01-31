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
            List<Setlist> setlistItems = new List<Setlist>();

            foreach (int songId in model.SongId)
            {
                var entity = new Setlist
                {
                    SongId = songId,
                    ConcertId = model.ConcertId
                };
                setlistItems.Add(entity);
            }

            using (var ctx = new ApplicationDbContext())
            {
                foreach(var item in setlistItems)
                {
                    ctx.Setlists.Add(item);
                }
                return ctx.SaveChanges() == 1;
            }
        }
        //Method changed to use ConcertId as key and list Concert ID as well as name.
        /*
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
        */
        public IEnumerable<SetlistListItem> GetAllSetLists()
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<SetlistListItem> setList = new List<SetlistListItem>();
                var query = ctx.Setlists.Include("Concert").AsEnumerable().GroupBy(s => s.Concert.ConcertId);
                foreach (var group in query)
                {
                    setList.Add(new SetlistListItem
                    {
                        SetlistItemIds = group.Select(s => s.SetlistId).ToList(),
                        ConcertId = group.Key,
                        ConcertName = group.Select(c => c.Concert.ConcertName).FirstOrDefault(),
                        SongCount = group.Count()
                    });
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
                    sets = ctx.Setlists.Include("Song").Where(s => s.ConcertId == concertId);
                }
                catch (Exception ex)
                {
                    return null;
                }
                if (sets == null)
                    return null;

                return new SetlistDetail
                {
                    SetlistItemIds = sets.Select(s => s.SetlistId).ToList(),
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
        public bool DeleteSetlistItem(int setlistId)
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

        public bool DeleteSetlist(int concertId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                List<Setlist> setlistItems;

                setlistItems = ctx.Setlists.Where(s => s.ConcertId == concertId).ToList();

                if (setlistItems.Count == 0)
                    return false;

                foreach (Setlist item in setlistItems)
                {
                    ctx.Setlists.Remove(item);
                }

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
