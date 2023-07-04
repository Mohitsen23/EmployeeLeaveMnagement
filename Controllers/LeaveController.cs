using Microsoft.AspNetCore.Mvc;
using Practice.NewFolder;
using Practice.Models;
namespace Practice.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController:ControllerBase
    {
        private readonly LeaveApplicationContext Leaveapp;
        public LeaveController(LeaveApplicationContext leaveapp)
        {
            Leaveapp = leaveapp;

        }


        [HttpPost("/applyleave")]
        public async Task<ActionResult> ApplyLeave(AppplyLeaveDTO leave)
        {
            Guid demoGuid = Guid.NewGuid();

           var applyleave = new LeaveStatus()
            {    Id=leave.Id,
                Leavetype = leave.Leavetype,
                Leaveid =leave.Leaveid,
                FromDate = leave.FromDate,
                ToDate = leave.ToDate,
                Reason = leave.Reason,
                Status = leave.Status,
                Manager = leave.Manager,
                Leave = leave.Leave

            };


            Leaveapp.LeaveStatuses.Add(applyleave);
            Leaveapp.SaveChanges();
            return Ok("Leave Apply Successfully");
        }





    }
}
