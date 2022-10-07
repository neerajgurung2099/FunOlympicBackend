using FunOlympicBackEnd.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace FunOlympicBackEnd.Controllers.Public
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private Helper helper;
        public PublicController(IConfiguration configuration)
        {
            helper = new Helper(configuration);
        }

        [HttpGet]
        public JsonResult GetCurrentLiveGames()
        {
            try
            {
                SqlParameter[] parm = {

                };
                string result = helper.ReadDataToJson("usp_Match_GetCurrentLiveGames", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }

    }
}
