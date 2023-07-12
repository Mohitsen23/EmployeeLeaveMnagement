   using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Models;
using Practice.NewFolder;
using System.Linq;

namespace Practice.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly LeaveApplicationContext Leaveapp;
        public EmployeeController(LeaveApplicationContext leaveapp )
        {
            Leaveapp = leaveapp;
        }
        [HttpPost("/empsignup")]
        public async Task<ActionResult> Employee( EmployeeDto mgr) 
        {

           
            var User = Leaveapp.Employees.Where(user => user.Email == mgr.Email).SingleOrDefault();
            if (User != null)
            {
                return BadRequest("User With Email Already Exist");

            }

            var employee = new Employee
            {  
                Firstname = mgr.Firstname,
                Lastname = mgr.Lastname,
                Email = mgr.Email,
                Password = mgr.Password,
                Department = mgr.Department,
                Companyname = mgr.Companyname,
                Manager = mgr.Manager,
                Status=mgr.Status
               
            };
           /* var leaveQuota = new LeaveQuotum
            {
                Emplid=4,
                Remainingleave = 20,
                Totalleave = 20,
                Usedleave = 0

            };
            Leaveapp.LeaveQuota.Add(leaveQuota);
           */
            Leaveapp.Employees.Add(employee);
            Leaveapp.SaveChanges();

            return Ok(employee);
        }

        [HttpPost("/emplogin")]
        public async Task<ActionResult<Employee>> EmployeeLogin(EmpLoginDto emp)
        {
            var user = Leaveapp.Employees.FirstOrDefault(u => u.Email == emp.Email);
            Console.Write("users data" + user);
            if (user != null)
            {
                if (user.Password == emp.Password)
                {
                    return user;
                }
            }

            return BadRequest("User Does Not Exist");
        }



       


        [HttpGet("/getUserByID/{id}")]
        public async Task<ActionResult<Employee>> getUserById(int id)
        {
            var user =await Leaveapp.Employees.FindAsync(id);
            if (user != null)
            {
                return user;
            }
            return BadRequest("User Not Found");

        }



        [HttpGet("/getEmployees")]
        public async Task<ActionResult<List<Employee>>> getEmployees()
        {
            string jwtKey = Startup.GenerateSecretKey(32);
            Console.Write("JWT " + jwtKey);
            List<Employee> EmployeesList = await Leaveapp.Employees.ToListAsync();

            List<Employee> FiltereEmployee = new List<Employee>();
            foreach(var emp in EmployeesList)
            {
                if(emp.Status!= "Removed             ")
                {
                    FiltereEmployee.Add(emp);
                }
            }
            if (FiltereEmployee != null)
            {
                return FiltereEmployee;
            }
            return BadRequest("No Employees");

        }

        [HttpGet("/getAllLeaves/{id}")]

        public async Task<ActionResult<List<LeaveStatus>>> getAllLeaves(int id)
        {
            var leavedata = Leaveapp.LeaveStatuses.Where(t => t.Emplid == id).ToList();

            if (leavedata != null)
            {
                return leavedata;
            }

            return BadRequest("No Request");
        }


        [HttpGet("/leaveQuota/{id}")]
       public async Task<ActionResult<LeaveQuotum>> leaveQuota(int id)
        {
            var Leavedetails=Leaveapp.LeaveQuota.FirstOrDefault(u => u.Emplid == id);
            if(Leavedetails != null)
            {
                return Leavedetails;
            }
            return BadRequest("Getting Error");
        }



        [HttpDelete("/deleteLeave/{id}")]
        public async Task<ActionResult> deleteLeave(int id)
        {
            var employee = Leaveapp.LeaveStatuses.FirstOrDefault(emp => emp.Id == id);
            if (employee != null)
            {

                Leaveapp.LeaveStatuses.Remove(employee);
                Leaveapp.SaveChanges();
                return Ok("Removed Successfully");

            }
            return BadRequest("Not Deleted");

        }




    }
}
