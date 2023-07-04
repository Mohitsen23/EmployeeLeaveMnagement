using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Models;
using Practice.NewFolder;
using System.ComponentModel;
using System.Linq;

namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly LeaveApplicationContext Leaveapp;
        public ManagerController(LeaveApplicationContext leaveapp)
        {
            Leaveapp = leaveapp;


        }
        [HttpPost]
        public async Task<ActionResult> manager([FromBody] ManagerDTO mgr)
        {
            if (mgr == null)
            {
                return BadRequest("please Insert the Data");
            }
            var User = Leaveapp.Managers.FirstOrDefault(user => user.Email == mgr.Email);
            if (User != null)
            {
                return BadRequest("User With Email Already Exist");

            }

            var manager = new Manager
            {
                Firstname = mgr.Firstname,
                Lastname = mgr.Lastname,
                Email = mgr.Email,
                Password = mgr.Password,
                Department = mgr.Department,
                Companyname = mgr.Companyname,
                Employees = mgr.Employees
            };
            Leaveapp.Managers.Add(manager);
            Leaveapp.SaveChanges();

            return Ok(manager);

        }



        [HttpPost("/mgrlogin")]
        public async Task<ActionResult<Employee>> managerLogin(MgrLoginDTO mgr)
        {
            var User = Leaveapp.Managers.FirstOrDefault(users => users.Email == mgr.Email);
            if (User != null)
            {
                if (User.Password == mgr.Password)
                {
                    return Ok("USer Login SuccesFull");
                }
            }

            return BadRequest("User Not Exist");
        }
        [HttpGet("/EmployeesbyMgrId/{id}")]
        public async Task<ActionResult<Employee>> EmployeesbyMgrId(int id)
        {
            var emp=await Leaveapp.Employees.FindAsync(id);
            if (emp != null)
            {
                return emp;
            }
            return BadRequest("No Employees Under Manager");
        }


        [HttpGet("/leaveRequest")]
        public async Task<ActionResult<List<LeaveStatus>>> AllLeaveRequest()
        {
            // Assuming you have a database context named "dbContext" with a DbSet for LeaveTable
            List<LeaveStatus> leaveRequests = await Leaveapp.LeaveStatuses.ToListAsync();

            List<LeaveStatus> FilteredRequest = new List<LeaveStatus>();
            foreach(var data in leaveRequests)
            {
                if (data.Status != "Approved                      ")
                {
                    FilteredRequest.Add(data);
                }
            }

            return FilteredRequest;
        }


        [HttpGet("/ChangeLeaveStatus/{leaveid}")]
        public async Task<IActionResult> UpdateLeaveRequest(int leaveid)
        {

        var LeaveData = await Leaveapp.LeaveStatuses.FirstOrDefaultAsync(leave => leave.Leaveid == leaveid);

            if (LeaveData != null)
            {
                LeaveData.Status = "Approved";
             var dataleave=await Leaveapp.LeaveTables.FirstOrDefaultAsync(leave => leave.Leaveid == leaveid);
               var data=await Leaveapp.LeaveQuota.FirstOrDefaultAsync(leavedata => leavedata.Emplid == dataleave.Employeeid);
                data.Remainingleave -=  1;
                data.Totalleave -= 1;
                data.Usedleave +=1;
                Leaveapp.SaveChanges();
                return Ok("Leave Approved");
            }    
              return BadRequest("Not Approved");
        }


        [HttpDelete("/deleteEmployee/{email}")]
        public async Task<ActionResult> deleteEmployee(String email)
        {
            var employee = Leaveapp.Employees.FirstOrDefault(emp => emp.Email == email);
            if (employee != null)
            {
                Leaveapp.Employees.Remove(employee);
                Leaveapp.SaveChanges();
                return Ok("Employee Deleted Successfully");
            }
            return BadRequest("Not Deleted");

    }
    }

    
    

}

