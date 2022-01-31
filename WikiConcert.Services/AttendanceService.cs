using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiConcert.Data;
using WikiConcert.Models;

namespace WikiConcert.Services
{
    public class AttendanceService
    {
        private readonly Guid _userId;

        public AttendanceService(Guid userId)
        {
            _userId = userId;
        }

        public bool AddAttendance(AttendanceAdd model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Attendancelist.Add(new Attendance
                {
                    ConcertId = model.ConcertId,
                    GuestId = _userId
                });

                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<AttendanceListItem> GetAttendance()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var attendanceList = ctx.Attendancelist.Include("Concert").Where(c => c.GuestId == _userId)
                    .Select(c => new AttendanceListItem
                    {
                        AttendanceId = c.AttendanceId,
                        concertId = c.ConcertId,
                        concertName = c.Concert.ConcertName
                    });

                return attendanceList.ToList();
            }
        }

        public bool RemoveAttendance(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Attendance entity;
                try
                {
                    entity = ctx.Attendancelist.Single(a => a.AttendanceId == id);
                }
                catch (Exception ex)
                {
                    return false;
                }
                ctx.Attendancelist.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

        public bool RemoveAttendanceByConcert(int concertId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                Attendance entity;
                try
                {
                    entity = ctx.Attendancelist.Single(a => a.ConcertId == concertId && a.GuestId == _userId);
                }
                catch (Exception ex)
                {
                    return false;
                }
                ctx.Attendancelist.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
