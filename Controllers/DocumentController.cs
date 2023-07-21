using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Models;


namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly LeaveApplicationContext _leaveApp;

        public DocumentController(LeaveApplicationContext leaveApp)
        {
            _leaveApp = leaveApp;
        }

        [HttpPost("/upload/{emplid}")]
        public IActionResult Upload(IFormFile file, int emplid)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    var fileData = memoryStream.ToArray();

                    var model = new Document
                    {
                        documentName = file.FileName,
                        File = fileData,
                        Emplid = emplid
                    };

                    _leaveApp.Documents.Add(model);
                    _leaveApp.SaveChanges();
                }
            }

            return Ok("Document Uploaded Successfully");
        }

        [HttpGet("/download/Document")]
        public async Task<ActionResult<List<Document>>> Download()
        {
            List<Document> document = await _leaveApp.Documents.ToListAsync();
            if (document != null)
            {
                return document;

                
            }
            return NotFound();
        }
    }
}
