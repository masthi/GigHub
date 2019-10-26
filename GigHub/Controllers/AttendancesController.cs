using System.Linq;
using System.Web.Http;
using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }
        
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto )
        {
            var userid = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userid && a.GigId == dto.GidId))
                return BadRequest("The attendance already exits");

            var attendance = new Attendance()
            {
                GigId = dto.GidId,
                AttendeeId = userid
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();
            return Ok();
        }
    }
}
