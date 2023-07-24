using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Practice.ChatHub;
using Practice.Models;

namespace Practice.Controllers
{
    public class ChatController:ControllerBase
    {
        private readonly IHubContext<ChatdataHub> _hubContext;
        public ChatController(IHubContext<ChatdataHub> _context)
        {
            _hubContext = _context;
        }
        [HttpPost("/SendMessage")]
        public async Task<IActionResult> sendMessage([FromBody] Messages message)
        {
                 return Ok();


        }
    }
}
