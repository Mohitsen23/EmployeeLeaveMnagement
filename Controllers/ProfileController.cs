using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Models;

namespace Practice.Controllers
{
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private LeaveApplicationContext _context;
        public ProfileController(LeaveApplicationContext context)
        {
            _context = context;
        }
        [HttpPost("/uploadProfile/{emplid}")]
        public ActionResult uploadProfile(IFormFile file, int emplid)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    var fileData = memoryStream.ToArray();

                    var model = new Profile
                    {
                        
                        img = fileData,
                        Emplid = emplid
                    };

                  _context.Profiles.Add(model);
                    _context.SaveChanges();
                }
                return Ok("profile uploaded succesfully");
            }
            return BadRequest();
        }
        [HttpGet("/download/Profile")]
        public async Task<ActionResult<List<Profile>>> Profile()
        {
            List<Profile> profile = await _context.Profiles.ToListAsync();
            if (profile != null)
            {
                return profile;


            }
            return NotFound();
        }
    }
}
