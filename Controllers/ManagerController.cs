using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Practice.Models;
using Practice.NewFolder;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly LeaveApplicationContext Leaveapp;
        private readonly IConfiguration _config;
        public ManagerController(LeaveApplicationContext leaveapp, IConfiguration config)
        {
            Leaveapp = leaveapp;
            _config = config;

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
              
            };
            Leaveapp.Managers.Add(manager);
            Leaveapp.SaveChanges();

            return Ok(manager);

        }



        [HttpPost("/mgrlogin")]
        public async Task<ActionResult<Manager>> ManagerLogin(MgrLoginDTO mgr)
        {
            var user = Leaveapp.Managers.SingleOrDefault(u => u.Email == mgr.Email);

            if (user != null && user.Password == mgr.Password)
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, mgr.Email),
            new Claim(ClaimTypes.Role, "Manager"),
            // Add any additional claims as needed
        };

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(1), // Use local time zone for expiration
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

                var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                return Ok(jwtToken);
            }

            return BadRequest("User does not exist");
        }



        [Authorize(Roles = "Manager")]
        [HttpPost("/mgrlogindata")]
        public async Task<ActionResult<Manager>> ManagerLoginData(MgrLoginDTO mgr)
        {
            var user = Leaveapp.Managers.SingleOrDefault(u => u.Email == mgr.Email);

            if (user != null && user.Password == mgr.Password)
            {
               
                return Ok(user);
            }

            return BadRequest("User does not exist");
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

        [Authorize(Roles = "Manager")]
        [HttpGet("/leaveRequest")]

       
        public async Task<ActionResult<List<LeaveStatus>>> AllLeaveRequest()
        {
            // Assuming you have a database context named "dbContext" with a DbSet for LeaveTable
            List<LeaveStatus> leaveRequests = await Leaveapp.LeaveStatuses.ToListAsync();

            List<LeaveStatus> FilteredRequest = new List<LeaveStatus>();
            foreach(var data in leaveRequests)
            {
               
            
                    FilteredRequest.Add(data);
               
            }

            return FilteredRequest;
        }


        [Authorize(Roles = "Manager")]
        [HttpGet("/RejectLeaveRequest/{leaveid}")]
        public async Task<IActionResult> RejectLeaveRequest(int leaveid)
        {

            var LeaveData = await Leaveapp.LeaveStatuses.FirstOrDefaultAsync(leave => leave.Leaveid == leaveid);

            if (LeaveData != null)
            {
                LeaveData.Status = "Rejected";
               
                Leaveapp.SaveChanges();
                return Ok("Leave Rejcted");
            }
            return BadRequest("Not Approved");
        }




        [Authorize(Roles = "Manager")]
        [HttpGet("/ChangeLeaveStatus/{leaveid}")]
        public async Task<IActionResult> UpdateLeaveRequest(int leaveid)
        {

        var LeaveData = await Leaveapp.LeaveStatuses.FirstOrDefaultAsync(leave => leave.Leaveid == leaveid);

            if (LeaveData != null)
            {
                LeaveData.Status = "Approved";
             
                Leaveapp.SaveChanges();
                return Ok("Leave Approved");
            }    
              return BadRequest("Not Approved");
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("/deleteEmployee/{email}")]
        public async Task<ActionResult> deleteEmployee(String email)
        {
            var employee = Leaveapp.Employees.FirstOrDefault(emp => emp.Email == email);
            if (employee != null)
            {

                employee.Status = "Removed";
                Leaveapp.SaveChanges();
                return Ok("Removed Successfully");
                
            }
            return BadRequest("Not Deleted");

    }
        [Authorize(Roles = "Manager")]
        [HttpPut("/updateEmployee/{id}")]
        public async Task<ActionResult> UpdateEmployees(int id, [FromBody] EmployeeDto emp)
        {
            Employee existingEmployee = await Leaveapp.Employees.FindAsync(id);

            if (existingEmployee != null)
            {
                existingEmployee.Firstname = emp.Firstname;
                existingEmployee.Lastname = emp.Lastname;
                existingEmployee.Email = emp.Email;
                existingEmployee.Password = emp.Password;
                existingEmployee.Department = emp.Department;
                existingEmployee.Companyname = emp.Companyname;

                existingEmployee.Manager = emp.Manager;

                Leaveapp.Update(existingEmployee);
                await Leaveapp.SaveChangesAsync();

                return Ok("User details updated");
            }

            return BadRequest("Employee not found");
        }





    }




}


