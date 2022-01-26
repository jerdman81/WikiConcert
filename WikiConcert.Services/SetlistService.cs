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

        public bool CreateSetlist(SetlistCreate model)
        {
            var entity = new Setlist
            {
                SongIds = model.SongIds,
                CreatedUtc = DateTimeOffset.Now
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
                var query = ctx.Setlists.Select(s => new SetlistListItem
                {
                    SetlistId = s.SetlistId,
                    Songs = s.Songs.Select(n => n.Name).ToList()
                });

                return query.ToList();
            }
        }

        public SetlistDetail GetSetlistById(int setlistId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Setlist query;
                try
                {
                    query = ctx.Setlists.Single(s => s.SetlistId == setlistId);
                }
                catch (Exception ex)
                {
                    return null;
                }
                return new SetlistDetail
                {
                    SetlistId = query.SetlistId,
                    Songs = query.Songs.Select(n => n.Name).ToList(),
                    CreatedUtc = query.CreatedUtc,
                    ModifiedUtc = query.ModifiedUtc
                };
            }
        }

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
    }
}
