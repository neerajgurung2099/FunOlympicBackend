using FunOlympicBackEnd.Classes;
using FunOlympicBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace FunOlympicBackEnd.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private Helper helper;
        public GameController(IConfiguration configuration)
        {
            helper = new Helper(configuration);
        }
        [HttpPost]
        public JsonResult InsertGameGroup(GameGroup game)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@GroupName",game.GroupName),
                    new SqlParameter("@GroupDescription",game.GroupDescription),
                    new SqlParameter("@View",game.View)
                };
                int AffectedRows= helper.InsertUpdateCn("usp_GameGroup_Insert", parm, CommandType.StoredProcedure);
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
        [HttpGet]
        public JsonResult GetAllGameGroup()
        {
            try
            {
                SqlParameter[] parm = {
                    
                };
                string result = helper.ReadDataToJson("usp_GameGroup_SelectAll", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }

        [HttpGet]
        public JsonResult GetAllGame()
        {
            try
            {
                SqlParameter[] parm = {

                };
                string result = helper.ReadDataToJson("usp_Game_SelectAll", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }

        [HttpPost]
        public JsonResult InsertGame(Game game)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@Category",game.Category),
                    new SqlParameter("@GroupId",game.GroupId),
                    new SqlParameter("@ParticipantType",game.ParticipantType),
                    new SqlParameter("@View",game.View),
                    new SqlParameter("@GameName",game.GameName),
                    new SqlParameter("@GameDescription",game.GameDescription),
                    new SqlParameter("@TotalMatches",game.TotalMatches),
                };
                int AffectedRows = helper.InsertUpdateCn("usp_Game_Insert", parm, CommandType.StoredProcedure);
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
        public JsonResult GetPlayerByGameId(Game game)
        {
            try
            {
                SqlParameter[] parm = {
                     new SqlParameter("@GameId",game.GameId),
                };
                string result = helper.ReadDataToJson("usp_tbPlayer_SelectByGameId", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }

        [HttpPost]
        public JsonResult GetTeamByGameId(Game game)
        {
            try
            {
                SqlParameter[] parm = {
                     new SqlParameter("@GameId",game.GameId),
                };
                string result = helper.ReadDataToJson("usp_tbTeam_SelectByGameId", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }
        [HttpPost]
        public JsonResult InsertMatch(Match match)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@GameId",match.GameId),
                    new SqlParameter("@View",match.View),
                    new SqlParameter("@MatchTitle",match.MatchTitle),
                    new SqlParameter("@LiveLink",match.LiveLink),
                    new SqlParameter("@StartTime",match.StartTime),
                    new SqlParameter("@StartDate",match.StartDate),
                };
                DataTable dt1 = new DataTable();
                dt1 = helper.ReadDataCn("usp_Match_Insert", parm, CommandType.StoredProcedure);
                int MatchId = Int32.Parse(dt1.Rows[0]["MatchId"].ToString());
                int AffectedRows;
                int TotalAffectedRows = 0;
                var JsonString = "";
                foreach (var item in match.matchParticipants)
                {
                    SqlParameter[] parm1 =
                    {
                        new SqlParameter("@MatchId",MatchId),
                        new SqlParameter("@ParticipantId",item.ParticipantId),
                    };
                    AffectedRows = helper.InsertUpdateCn("usp_MatchParticipant_Insert", parm1, CommandType.StoredProcedure);
                    TotalAffectedRows +=  AffectedRows;
                }
                if(TotalAffectedRows > 0)
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

        [HttpGet]
        public JsonResult GetAllMatches()
        {
            try
            {
                SqlParameter[] parm = {

                };
                string result = helper.ReadDataToJson("usp_Match_SelectAll", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }

    }
}
