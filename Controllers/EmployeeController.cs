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

           
            var User = Leaveapp.Employees.FirstOrDefault(user => user.Email == mgr.Email);
            if (User != null)
            {
                return BadRequest("User With Email Already Exist");

            }

            var employee = new Employee
            {  Id=mgr.Id,
                Firstname = mgr.Firstname,
                Lastname = mgr.Lastname,
                Email = mgr.Email,
                Password = mgr.Password,
                Department = mgr.Department,
                Companyname = mgr.Companyname,
                Manager = mgr.Manager,
                LeaveQuota=mgr.LeaveQuota
            };
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
                    return Ok("User Login Successful");
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
            List<Employee> EmployeesList = await Leaveapp.Employees.ToListAsync();
            if (EmployeesList != null)
            {
                return EmployeesList;
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


        



    }
}
