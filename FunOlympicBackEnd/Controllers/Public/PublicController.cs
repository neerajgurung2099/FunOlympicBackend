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
                    groupArr.Add(new GameGroup
                    {
                        GroupId = int.Parse(dt1.Rows[i]["Group_Id"].ToString()),
                        GroupName = dt1.Rows[i]["Group_Name"].ToString(),
                        Games = new List<Game>()
                    });

                    SqlParameter[] parm1 = {
                        new SqlParameter("@GroupId" , dt1.Rows[i]["Group_Id"].ToString())
                    };

                    dt2 = helper.ReadDataCn("usp_Game_SelectByGroupId", parm1, CommandType.StoredProcedure);

                    for (var j=0; j<dt2.Rows.Count; j++)
                    {
                        groupArr[i].Games.Add(new Game
                        {
                            GameId = int.Parse(dt2.Rows[j]["Game_Id"].ToString()),
                            GameName = dt2.Rows[j]["Game_Name"].ToString(),
                            matches = new List<Match>()
                        });

                        SqlParameter[] parm2 = {
                        new SqlParameter("@GameId" , dt2.Rows[j]["Game_Id"].ToString())
                        };
                        dt3 = helper.ReadDataCn("usp_Match_SelectCurrentLiveByGameId", parm2, CommandType.StoredProcedure);

                        for(var z = 0; z < dt3.Rows.Count; z++)
                        {
                            groupArr[i].Games[j].matches.Add(new Match
                            {
                                MatchId = int.Parse(dt3.Rows[z]["Match_Id"].ToString()),
                                MatchTitle = dt3.Rows[z]["Match_Title"].ToString()
                            });
                        }
                    }
                }

                return new JsonResult(Ok(groupArr));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }


        [HttpGet]
        public JsonResult GetUpcomingLiveSchedule()
        {
            try
            {
                List<GameGroup> groupArr = new List<GameGroup>();
                dynamic dynObject = new ExpandoObject();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                SqlParameter[] parm = {

                };
                dt1 = helper.ReadDataCn("usp_GameGroup_SelectAll", parm, CommandType.StoredProcedure);

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    groupArr.Add(new GameGroup
                    {
                        GroupId = int.Parse(dt1.Rows[i]["Group_Id"].ToString()),
                        GroupName = dt1.Rows[i]["Group_Name"].ToString(),
                        Games = new List<Game>()
                    });

                    SqlParameter[] parm1 = {
                        new SqlParameter("@GroupId" , dt1.Rows[i]["Group_Id"].ToString())
                    };

                    dt2 = helper.ReadDataCn("usp_Game_SelectByGroupId", parm1, CommandType.StoredProcedure);

                    for (var j = 0; j < dt2.Rows.Count; j++)
                    {
                        groupArr[i].Games.Add(new Game
                        {
                            GameId = int.Parse(dt2.Rows[j]["Game_Id"].ToString()),
                            GameName = dt2.Rows[j]["Game_Name"].ToString(),
                            matches = new List<Match>()
                        });

                        SqlParameter[] parm2 = {
                        new SqlParameter("@GameId" , dt2.Rows[j]["Game_Id"].ToString())
                        };
                        dt3 = helper.ReadDataCn("usp_Match_SelectUpComingLiveByGameId", parm2, CommandType.StoredProcedure);

                        for (var z = 0; z < dt3.Rows.Count; z++)
                        {
                            groupArr[i].Games[j].matches.Add(new Match
                            {
                                MatchId = int.Parse(dt3.Rows[z]["Match_Id"].ToString()),
                                MatchTitle = dt3.Rows[z]["Match_Title"].ToString(),
                                StartDate = dt3.Rows[z]["Start_Date"].ToString(),
                                StartTime = dt3.Rows[z]["Start_Time"].ToString()
                            });
                        }
                    }
                }

                return new JsonResult(Ok(groupArr));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }

        [HttpPost]
        public JsonResult GetNewsDetailById(News news)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@NewsId" , news.NewsId)
                };
                string result = helper.ReadDataToJson("usp_News_SelectById", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }

        [HttpGet]
        public JsonResult GetAllNews()
        {
            try
            {
                SqlParameter[] parm = {
                };
                string result = helper.ReadDataToJson("usp_News_Public_SelectAllForPublic", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }

        [HttpGet]
        public JsonResult GetGameMatchList()
        {
            try
            {
                List<GameGroup> groupArr = new List<GameGroup>();
                dynamic dynObject = new ExpandoObject();
                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();
                SqlParameter[] parm = {

                };
                dt1 = helper.ReadDataCn("usp_GameGroup_SelectAll", parm, CommandType.StoredProcedure);

                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    groupArr.Add(new GameGroup
                    {
                        GroupId = int.Parse(dt1.Rows[i]["Group_Id"].ToString()),
                        GroupName = dt1.Rows[i]["Group_Name"].ToString(),
                        Games = new List<Game>()
                    });

                    SqlParameter[] parm1 = {
                        new SqlParameter("@GroupId" , dt1.Rows[i]["Group_Id"].ToString())
                    };

                    dt2 = helper.ReadDataCn("usp_Game_SelectByGroupId", parm1, CommandType.StoredProcedure);

                    for (var j = 0; j < dt2.Rows.Count; j++)
                    {
                        groupArr[i].Games.Add(new Game
                        {
                            GameId = int.Parse(dt2.Rows[j]["Game_Id"].ToString()),
                            GameName = dt2.Rows[j]["Game_Name"].ToString(),
                            matches = new List<Match>()
                        });

                        SqlParameter[] parm2 = {
                        new SqlParameter("@GameId" , dt2.Rows[j]["Game_Id"].ToString())
                        };
                        dt3 = helper.ReadDataCn("usp_Match_SelectEndedMatches", parm2, CommandType.StoredProcedure);

                        for (var z = 0; z < dt3.Rows.Count; z++)
                        {
                            groupArr[i].Games[j].matches.Add(new Match
                            {
                                MatchId = int.Parse(dt3.Rows[z]["Match_Id"].ToString()),
                                MatchTitle = dt3.Rows[z]["Match_Title"].ToString(),
                                StartDate = dt3.Rows[z]["Start_Date"].ToString(),
                                StartTime = dt3.Rows[z]["Start_Time"].ToString()
                            });
                        }
                    }
                }

                return new JsonResult(Ok(groupArr));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }
        [HttpPost]
        public JsonResult GetGroupAndMatchDetail(Match match)
        {
            try
            {
                SqlParameter[] parm = {
                    new SqlParameter("@MatchId" , match.MatchId)
                };
                string result = helper.ReadDataToJson("usp_Match_SelectGroupAndMatchDetailsByMatchId", parm, CommandType.StoredProcedure);
                return new JsonResult(Ok(result));
            }
            catch (Exception ex)
            {
                return new JsonResult(BadRequest());
            }
        }

       


    }
}
