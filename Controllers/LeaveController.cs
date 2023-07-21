using Microsoft.AspNetCore.Mvc;
using Practice.NewFolder;
using Practice.Models;
using Microsoft.AspNetCore.SignalR;
using Practice.Nofication;

namespace Practice.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly LeaveApplicationContext Leaveapp;
        private readonly IHubContext<NotificationHub> _hubContext;
        public LeaveController(LeaveApplicationContext leaveapp, IHubContext<NotificationHub> hubContext)
        {
            Leaveapp = leaveapp;
            _hubContext = hubContext;

        }


        [HttpPost("/applyleave")]
        public async Task<ActionResult> ApplyLeave(AppplyLeaveDTO leave)
        {

            Console.WriteLine(leave);
            Guid demoGuid = Guid.NewGuid();

            var applyleave = new LeaveStatus()
            {
                Leavetype = leave.Leavetype,
                Emplid = leave.Emplid,
                FromDate = leave.FromDate,
                ToDate = leave.ToDate,
                Reason = leave.Reason,
                Status = leave.Status,
                Manager = leave.Manager,
                Leave = leave.Leave

            };
  

            Leaveapp.LeaveStatuses.Add(applyleave);
            Leaveapp.SaveChanges();
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", "New leave request received");
            return Ok("Leave Apply Successfully");
        }



       
    }
}
