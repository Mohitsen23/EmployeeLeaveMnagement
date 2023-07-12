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

            Console.WriteLine(leave);
            Guid demoGuid = Guid.NewGuid();

           var applyleave = new LeaveStatus()
            {    
                Leavetype = leave.Leavetype,
                  Emplid=leave.Emplid,
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



/*
    var dataleave = await Leaveapp.LeaveTables.FirstOrDefaultAsync(leave => leave.Leaveid == leaveid);
    var data = await Leaveapp.LeaveQuota.FirstOrDefaultAsync(leavedata => leavedata.Emplid == dataleave.Employeeid);
    data.Remainingleave -= 1;
    data.Totalleave -= 1;
    data.Usedleave += 1;
*/

}
}
