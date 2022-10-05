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

namespace FunOlympicBackEnd.Controllers
{
    [Route("api/[controller]")]
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
                string Block = helper.ReadDataToJson("usp_UserLogin_VerifyUser", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(Block));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }

        }
    }
}
