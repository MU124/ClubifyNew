using Clubify.Models;
using DAE.DAL.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MSConfiguration;
using System.Data;
using System;
using System.Net.Mail;

namespace Clubify.Controllers
{
    //[Authorize]
    [ApiController]
    public class InvitationController : Controller
    {
        private readonly IMSConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public InvitationController(IMSConfigManager configuration, IWebHostEnvironment env)
        {
            this._configurationIG = configuration;
            this._env = env;
        }



        [Route("Invitation/Team")]
        [HttpPost]
        public IActionResult teamInvitation()
        {
            try
            {               
                // Create a new MailMessage instance
                MailMessage message = new MailMessage();

                // Set the sender email address
                message.From = new MailAddress("info@metasync.in");

                // Set the recipient email address
                message.To.Add("mayurutekar124@gmail.com");              

                // Set the email subject
                message.Subject = "Invitation Test";

                string htmlBody = @"
        <html>
        <head>
            <title>Invitation</title>
            <style>
                body {
                    font-family: Arial, sans-serif;
                    color: #6c757d;
                    background-color: #f8f9fa;
                    margin: 0;
                    padding: 0;
                }
                .container {
                    max-width: 600px;
                    margin: auto;
                    padding: 20px;
                    background-color: #ffffff;
                    border-radius: 10px;
                    box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                }
                h1 {
                    color: #343a40;
                }
                p {
                    margin-bottom: 15px;
                }
                ul {
                    margin: 0;
                    padding-left: 20px;
                }
                .button {
                    background-color: #007bff;
                    color: #ffffff;
                    border: none;
                    padding: 10px 20px;
                    border-radius: 5px;
                    cursor: pointer;
                    font-size: 16px;
                }
                button:hover {
                    background-color: #0056b3;
                }
            </style>
        </head>
        <body>
            <div class='container'>
                <h1>Invitation to Join the Team</h1>
                <p>Dear Mayur,</p>
                <p>You have been invited to join the team <strong>Team One</strong>.</p>
                <p><strong>Team Details:</strong></p>
                <ul>
                    <li>Team Name: Team One</li>
                    <!-- Add other team details as needed -->
                </ul>

                <input type=""text""/>
                 <br/>
                <br/>
                    <a href='http://localhost:12114/AccetpInvitation/Team' class='button'>Accept Invitation </a>  
            </div>
        </body>
        </html>";
                // Set the email body
                message.Body = htmlBody;
                message.IsBodyHtml = true;

                // Create an instance of the SmtpClient class
                SmtpClient smtpClient = new SmtpClient("mail.metasync.in", 8889); // Replace with your SMTP server details

                // Set your SMTP server credentials if required
                smtpClient.Credentials = new System.Net.NetworkCredential("info@metasync.in", "SN24P@ssw0rd"); // Replace with your credentials

                // Enable SSL encryption if required
                smtpClient.EnableSsl = false;
                

                try
                {
                    // Send the email
                    smtpClient.Send(message);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions
                    //Console.WriteLine($"Error sending email: {ex.Message}");
                }
                finally
                {
                    // Dispose of the SmtpClient and MailMessage objects
                    smtpClient.Dispose();
                    message.Dispose();
                }

                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok();
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("AccetpInvitation/Team")]
        [HttpGet]
        public IActionResult acceptInvitation()
        {
            // Construct HTML response
            string htmlContent = @"
                <!DOCTYPE html>
                <html>
                <head>
                    <title>Thank You!</title>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                            text-align: center;
                        }
                        .container {
                            max-width: 600px;
                            margin: 50px auto;
                            padding: 20px;
                            background-color: #fff;
                            border-radius: 10px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }
                        h1 {
                            color: #333;
                        }
                        p {
                            color: #666;
                        }
                        .btn {
                            display: inline-block;
                            padding: 10px 20px;
                            background-color: #007bff;
                            color: #fff;
                            text-decoration: none;
                            border-radius: 5px;
                        }
                        .btn:hover {
                            background-color: #0056b3;
                        }
                    </style>
                </head>
                <body>
                    <div class=""container"">
                        <h1>Thank You!</h1>
                        <p>Your data has been successfully inserted.</p>
                        <a href=""http://localhost:4200/"" class=""btn"">Login</a>
                    </div>
                </body>
                </html>
            ";

            // Return HTML content
            return Content(htmlContent, "text/html");
        }

    }
}
