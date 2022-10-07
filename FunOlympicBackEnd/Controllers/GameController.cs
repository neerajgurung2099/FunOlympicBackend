using FunOlympicBackEnd.Classes;
using FunOlympicBackEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;

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
        
        [HttpGet]
        public JsonResult GetAllCountry()
        {
            try
            {
                SqlParameter[] parm = {

                };
                string result = helper.ReadDataToJson("usp_Country_SelectAll", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }

        [HttpPost]
        public JsonResult InsertPlayer(Player player)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@CountryId",player.CountryId),
                    new SqlParameter("@GroupId",player.GroupId),
                    new SqlParameter("@PlayerName",player.PlayerName),
                    new SqlParameter("@PlayerDescription",player.PlayerDescription),
                    
                };
                int AffectedRows = helper.InsertUpdateCn("usp_Player_Insert", parm, CommandType.StoredProcedure);
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
        public JsonResult GetAllPlayer()
        {
            try
            {
                SqlParameter[] parm = {

                };
                string result = helper.ReadDataToJson("usp_Player_SelectAll", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }

        [HttpGet]
        public JsonResult GetAllTeam()
        {
            try
            {
                SqlParameter[] parm = {

                };
                string result = helper.ReadDataToJson("usp_Team_SelectAll", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }
        [HttpPost]
        public JsonResult InsertTeam(Team team)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@CountryId",team.CountryId),
                    new SqlParameter("@GroupId",team.GroupId),
                    new SqlParameter("@TeamName",team.TeamName),
                    new SqlParameter("@TeamDescription",team.TeamDescription)

                };
                int AffectedRows = helper.InsertUpdateCn("usp_Team_Insert", parm, CommandType.StoredProcedure);
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
        public JsonResult InsertCountry(Country country)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@CountryName",country.CountryName)

                };
                int AffectedRows = helper.InsertUpdateCn("usp_Country_Insert", parm, CommandType.StoredProcedure);
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
        public JsonResult GetAllResult()
        {
            try
            {
                SqlParameter[] parm = {

                };
                string result = helper.ReadDataToJson("usp_MatchResult_SelectAll", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }

        [HttpPost]
        public JsonResult GetMatchParticipantByMatchId(Match match)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@MatchId",match.MatchId)
                };
                string result = helper.ReadDataToJson("usp_MatchParticipants_SelectByMatchId", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }


        [HttpPost]
        public JsonResult InsertMatchPoints(MatchParticipantList match)
        {
            try
            {
                
                int AffectedRows;
                int TotalAffectedRows = 0;
                var JsonString = "";
                foreach (var item in match.matchParticipants)
                {
                    SqlParameter[] parm = {
                    new SqlParameter("@MatchParticipantId",item.MatchParticipantId),
                    new SqlParameter("@Points",item.Points),
                    };
                    AffectedRows = helper.InsertUpdateCn("usp_MatchParticipants_InsertPoints", parm, CommandType.StoredProcedure);
                    TotalAffectedRows += AffectedRows;
                }
                if (TotalAffectedRows > 0)
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
        public JsonResult InsertImageIntoGallery([FromForm]Gallery gallery)
        {
            try
            {
                ImageHelper image = new ImageHelper();
                DataTable dt = new DataTable();
                Task<string> imagePath = image.UploadImage(gallery.Image);

                SqlParameter[] parm1 = {
                    new SqlParameter("@ImageLink",imagePath.Result),
                    };
                dt = helper.ReadDataCn("usp_Image_Insert", parm1, CommandType.StoredProcedure);
                int ImageId = int.Parse(dt.Rows[0]["ImageId"].ToString());

                int AffectedRows;
                var JsonString = "";
                SqlParameter[] parm = {
                new SqlParameter("@ImageId",ImageId),
                new SqlParameter("@View",gallery.View),
                };
                AffectedRows = helper.InsertUpdateCn("usp_Gallery_Insert", parm, CommandType.StoredProcedure);
                if (AffectedRows > 0)
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
        public JsonResult GetAllImage()
        {
            try
            {
                SqlParameter[] parm = {

                };
                string result = helper.ReadDataToJson("usp_Gallery_SelectAll", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }
        [HttpPost]
        public JsonResult DeleteGalleryImageById(MatchParticipant match)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@GalleryId",match.MatchParticipantId),
                };
                int AffectedRows;
                var JsonString = "";
                AffectedRows = helper.InsertUpdateCn("usp_Gallery_DeleteById", parm, CommandType.StoredProcedure);
                if (AffectedRows > 0)
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





    }
}
