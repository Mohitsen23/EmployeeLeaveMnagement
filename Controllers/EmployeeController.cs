   using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice.Models;
using Practice.NewFolder;
using System.Net.Mail;

using System.Linq;
using System.Net;
using static System.Net.WebRequestMethods;

namespace Practice.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private static Random random = new Random();
        private readonly LeaveApplicationContext Leaveapp;
        public EmployeeController(LeaveApplicationContext leaveapp)
        {
            Leaveapp = leaveapp;
        }
        [HttpPost("/empsignup")]
        public async Task<ActionResult> Employee(EmployeeDto mgr)
        {


            var User = Leaveapp.Employees.Where(user => user.Email == mgr.Email).SingleOrDefault();
            if (User != null)
            {
                return BadRequest("User With Email Already Exist");

            }

            Guid guid = Guid.NewGuid();
            byte[] bytes = guid.ToByteArray();
            int uniqueInteger = BitConverter.ToInt32(bytes, 0);

            var employee = new Employee
            {

                Firstname = mgr.Firstname,
                Lastname = mgr.Lastname,
                Email = mgr.Email,
                Password = mgr.Password,
                Department = mgr.Department,
                Companyname = mgr.Companyname,
                Manager = mgr.Manager,
                Status = mgr.Status,


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
                    return user;
                }
            }

            return BadRequest("User Does Not Exist");
        }







        static string GenerateOTP(int length)
        {
            const string digits = "0123456789";
            Random random = new Random();
            char[] otpChars = new char[length];

            for (int i = 0; i < length; i++)
            {
                otpChars[i] = digits[random.Next(digits.Length)];
            }

            return new string(otpChars);
        }











        [HttpPost("/generateOTP/{email}")]
        public ActionResult generateOTP(String email)
        {
            var eml = email + "          ";
            var user = Leaveapp.Employees.FirstOrDefault((emp) => emp.Email == eml);


           if(user != null) {
                string senderEmail = "mohitsen623@gmail.com";
                string senderPassword = "bhkwcboukeatroij";
                int otpLength = 4;
                string otp = GenerateOTP(otpLength);
                try
                {
                    MailMessage mail = new MailMessage(senderEmail, email);
                    mail.Subject = "OTP for Login";

                    mail.Body = otp;

                    // Construct the HTML body with the provided content
                    string htmlBody = $@"
                    <html>
                    <head>
                        <style>
                            /* Add your custom CSS styles here */
                            .body {{
                                font-family: Arial, sans-serif;
                                background-color: #e9eae3;
                                padding: 10px;
                            }}

                            h1 {{
                                color: #ff0000;
                                text-align: center;
                            }}

                            p {{
                                font-size: 18px;
                                margin-bottom: 10px;
                            }}
                       .custom-div {{
                                /* Add your custom styles here */
                                 background-color: #fff;
                                 padding:10px;
                               
                            }}
                         .heading {{
                            text-align:center;
                            color:blue;
                            
                          }}
                          
                        </style>
                    </head>
                    <body>
                    <div class=""body"">
                 
                   <h3 class={{heading}}>Nexgile Technologies</h3>

                    <div class=""custom-div"">
                     

                       <p>



                  ONE Time Password for Login
</p>
           
                     
                      {mail.Body}

                    </div>
</div>
                    </body>
                    </html>";

                    // Set the HTML body
                    mail.Body = htmlBody;
                    mail.IsBodyHtml = true;

                    // Create a new SmtpClient and specify the SMTP server settings
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);

                    // Send the email
                    smtpClient.Send(mail);

                    return Ok(otp);
                }

                catch (Exception ex)
                {
                    return BadRequest("Failed to send email: " + ex.Message);
                }

            }
            return BadRequest();



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
