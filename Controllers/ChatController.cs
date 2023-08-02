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

       
    }
}
