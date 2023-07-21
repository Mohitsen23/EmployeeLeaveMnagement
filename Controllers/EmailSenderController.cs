using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Practice.Models;

namespace Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailSenderController : ControllerBase
    {
        public EmailSenderController()
        {
            
        }
        [HttpPost("/sendEmail")]
        public IActionResult SendEmail([FromBody] EmailData email)
        {
            // Sender details
            string senderEmail = "mohitsen623@gmail.com";
            string senderPassword = "bhkwcboukeatroij";

            try
            {
                // Create a new MailMessage object



                MailMessage mail = new MailMessage(senderEmail, email.recipientEmail);
                mail.Subject = email.subject;

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
                    <img class=""img""custom-image"" src=""https://www.passionateinmarketing.com/wp-content/uploads/2021/09/Online-Upskilling-Platform-Scaler-Academy-launches-Forever-1068x601.jpg
                    "" alt=""Image"" />

                    <div class=""custom-div"">
                     

                       <p>



                    Hey! <br>
                    To help you in your career journey, we have lined up exclusive free events for you in the coming week: <br>
                    1. Monolithic and Microservices architecture <br>
                    Register to Learn the best practices for selecting the right architecture.<br>
                    Date: July 20th, Thursday, at 8 PM</P> <br>

     
                     


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

                return Ok("Email sent successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to send email: " + ex.Message);
            }
        }
    }
}
