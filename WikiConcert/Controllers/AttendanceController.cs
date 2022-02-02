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
    public class AttendanceController : ApiController
    {
        private AttendanceService CreateService()
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
            var service = new AttendanceService(userId);
            return service;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var service = CreateService();
            if(service == null)
                return Unauthorized();
            return Ok(service.GetAttendance());
        }
        [HttpPost]
        public IHttpActionResult Post(AttendanceAdd attend)
        {
            var service = CreateService();
            if (service == null)
                return Unauthorized();
            try
            {
                if (service.AddAttendance(attend))
                    return Ok("Successfully logged attendance.");
            }
            catch(Exception)
            {
                return BadRequest("Invalid concert ID.");
            }
            return InternalServerError();
        }
        [HttpDelete]
        public IHttpActionResult Delete(int attendId)
        {
            var service = CreateService();
            if (service == null)
                return Unauthorized();
            try
            {
                if (service.RemoveAttendance(attendId))
                    return Ok("Successfully removed attendance.");
            }
            catch
            {
                return BadRequest("Target attendance record not found.");
            }
            return InternalServerError();
        }
        [HttpDelete, ActionName("DeleteByConcert")]
        public IHttpActionResult DeleteByConcert(int concertId)
        {
            var service = CreateService();
            if (service == null)
                return Unauthorized();
            try
            {
                if (service.RemoveAttendanceByConcert(concertId))
                    return Ok("Successfully removed attendance.");
            }
            catch
            {
                return BadRequest("Target attendance record not found.");
            }
            return InternalServerError();
        }
    }
}
