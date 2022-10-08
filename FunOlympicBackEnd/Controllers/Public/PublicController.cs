using FunOlympicBackEnd.Classes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using FunOlympicBackEnd.Models;
using System.Dynamic;

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

        [HttpGet]
        public JsonResult GetCurrentLiveSchedule()
        {
            try
            {
                List<GameGroup> groupArr =new List<GameGroup>();
                dynamic dynObject = new ExpandoObject();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                SqlParameter[] parm = {

                };
                dt1 = helper.ReadDataCn("usp_GameGroup_SelectAll", parm, CommandType.StoredProcedure);
                for(int i=0; i < dt1.Rows.Count; i++)
                {
                    SqlParameter[] parm1 = {
                        new SqlParameter("@Category" , dt1.Rows[i]["Group_Id"].ToString())
                        };
                    dt2 = helper.ReadDataCn("usp_Game_SelectByGroupId", parm, CommandType.StoredProcedure);

                    for(var j=0; j<dt2.Rows.Count; j++)
                    {
                        SqlParameter[] parm2 = {
                        new SqlParameter("@Category" , dt2.Rows[j]["Game_Id"].ToString())
                        };
                        dt2 = helper.ReadDataCn("usp_Match_SelectByGameId", parm2, CommandType.StoredProcedure);
                    }
                }
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }

    }
}
