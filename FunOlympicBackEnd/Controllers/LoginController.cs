using FunOlympicBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Data;
using FunOlympicBackEnd.Classes;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Net.Mail;

namespace FunOlympicBackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private Helper helper;
        public LoginController(IConfiguration configuration)
        {
            helper = new Helper(configuration);
        }
        [HttpPost]
        public JsonResult UserLogin(User user)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@Email",user.Email),
                    new SqlParameter("@UserPassword",user.UserPassword)
                };
                string Block = helper.ReadDataToJson("usp_UserLogin_VerifyUser", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(Block));
            }
            catch(Exception ex)
            {
                return new JsonResult(BadRequest());
            }

        }

        [HttpPost]
        public JsonResult AdminLogin(User user)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@Email",user.Email),
                    new SqlParameter("@UserPassword",user.UserPassword)
                };
                string Block = helper.ReadDataToJson("usp_AdminLogin_VerifyUser", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(Block));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }

        }

        [HttpPost]
        public JsonResult RegisterUser(User user)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@Email",user.Email),
                    new SqlParameter("@UserPassword",user.UserPassword),
                    new SqlParameter("@UserName",user.UserName),
                };
                int  AffectedRows = helper.InsertUpdateCn("usp_UserLogin_Insert", parm, CommandType.StoredProcedure);
                var JsonString = "";
                if (AffectedRows == 1)
                {
                    JsonString = "{\"Status\":200}";
                }
                else
                {
                    JsonString = "{\"Status\":409}";
                }
                return new JsonResult(Ok(JsonString));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }

        }

        [HttpPost]
        public JsonResult VerifyEmail(User user)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@Email",user.Email),
                };
                string Block = helper.ReadDataToJson("usp_UserLogin_VerifyEmail", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(Block));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }

        }


        [HttpPost]
        public JsonResult ResetPassword(User user)
        {
            try
            {
                string NewPassword = "L@xmir1n1";
                SqlParameter[] parm = {
                    new SqlParameter("@Email",user.Email),
                };
                int AffectedRows  = helper.InsertUpdateCn("usp_UserLogin_ResetPassword", parm, CommandType.StoredProcedure);
                MailMessage mail = new MailMessage();
                mail.To.Add(user.Email);
                mail.From = new MailAddress("neeraj@insoftnepal.com");
                mail.Subject = "New Password";
                string Body = NewPassword;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false; 
                smtp.Credentials = new System.Net.NetworkCredential("neeraj@insoftnepal.com", "fhnooeuvqfwefukp"); // Enter senders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
                var  JsonString = "";
                JsonString = "{\"Status\":200}";
                return new JsonResult(Ok(JsonString));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }

        }


        [HttpPost]
        public JsonResult AdminResetPassword(User user)
        {
            try
            {
                string NewPassword = "L@xmir1n1";
                SqlParameter[] parm = {
                };
                int AffectedRows = helper.InsertUpdateCn("usp_AdminLogin_ResetPassword", parm, CommandType.StoredProcedure);
                MailMessage mail = new MailMessage();
                mail.To.Add("neerajgurung2099@gmail.com");
                mail.From = new MailAddress("neeraj@insoftnepal.com");
                mail.Subject = "New Password";
                string Body = NewPassword;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential("neeraj@insoftnepal.com", "fhnooeuvqfwefukp"); // Enter senders User name and password  
                smtp.EnableSsl = true;
                smtp.Send(mail);
                var JsonString = "";
                JsonString = "{\"Status\":200}";
                return new JsonResult(Ok(JsonString));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }

        }





    }
}
