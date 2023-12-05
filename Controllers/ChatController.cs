using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Models;


namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ChatController : ControllerBase
    {
      
        private readonly LeaveApplicationContext leaveapp; 
        public ChatController(LeaveApplicationContext _leaveapp)
        {
           
            leaveapp = _leaveapp;
        }

        [HttpPost("/SendMessage")]
        public IActionResult sendMessageData( MessageModel msgdata)
        {
            try
            {
                    var message = new MessageModel
                {
                    senderid = msgdata.senderid,
                    receiverid = msgdata.receiverid,
                    ReadorNot = msgdata.ReadorNot,
                    Message = msgdata.Message,
                    timestamp = msgdata.timestamp,
                };
                leaveapp.MessagesModels.Add(message);
                leaveapp.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }


    }
}
