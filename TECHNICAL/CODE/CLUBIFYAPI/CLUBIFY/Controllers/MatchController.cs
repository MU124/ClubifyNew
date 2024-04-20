using Clubify.Models;
using DAE.DAL.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using MSConfiguration;

namespace Clubify.Controllers
{
    [Authorize]
    [ApiController]
    public class MatchController : Controller
    {
        private readonly IMSConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public MatchController(IMSConfigManager configuration, IWebHostEnvironment env)
        {
            this._configurationIG = configuration;
            this._env = env;
        }

        [Route("Match/Get")]
        [HttpPost]
        public IActionResult Get(Match match)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);

                if (match.MatchID != 0)
                {
                    oDBUtility.AddParameters("@match_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, match.MatchID);
                }

                if (match.MatchDate != null)
                {
                    oDBUtility.AddParameters("@match_date", DBUtilDBType.Date, DBUtilDirection.In, 10, match.MatchDate);
                }

                if (!string.IsNullOrEmpty(match.Location))
                {
                    oDBUtility.AddParameters("@location", DBUtilDBType.Varchar, DBUtilDirection.In, 100, match.Location);
                }

                if (match.Fees != null)
                {
                    oDBUtility.AddParameters("@fees", DBUtilDBType.Numeric, DBUtilDirection.In, 18, match.Fees);
                }

                if (match.Team1ID != null)
                {
                    oDBUtility.AddParameters("@team1_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, match.Team1ID);
                }

                if (match.Team2ID != null)
                {
                    oDBUtility.AddParameters("@team2_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, match.Team2ID);
                }

                if (match.WinnerTeamID != null)
                {
                    oDBUtility.AddParameters("@winner_team_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, match.WinnerTeamID);
                }

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_GET_MATCH");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Match/Insert")]
        [HttpPost]
        public IActionResult Post(Match match)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@match_date", DBUtilDBType.Date, DBUtilDirection.In, 10, match.MatchDate);
                oDBUtility.AddParameters("@location", DBUtilDBType.Varchar, DBUtilDirection.In, 100, match.Location);
                oDBUtility.AddParameters("@fees", DBUtilDBType.Numeric, DBUtilDirection.In, 18, match.Fees);
                oDBUtility.AddParameters("@team1_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, match.Team1ID);
                oDBUtility.AddParameters("@team2_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, match.Team2ID);
                oDBUtility.AddParameters("@winner_team_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, match.WinnerTeamID);
                oDBUtility.AddParameters("@created_date", DBUtilDBType.DateTime, DBUtilDirection.In, 10, DateTime.Now);
                oDBUtility.AddParameters("@created_by", DBUtilDBType.Integer, DBUtilDirection.In, 10, match.CreatedBy);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_INSERT_MATCH");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Match/Update")]
        [HttpPost]
        public IActionResult Put(Match match)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@match_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, match.MatchID);
                oDBUtility.AddParameters("@match_date", DBUtilDBType.Date, DBUtilDirection.In, 10, match.MatchDate);
                oDBUtility.AddParameters("@location", DBUtilDBType.Varchar, DBUtilDirection.In, 100, match.Location);
                oDBUtility.AddParameters("@fees", DBUtilDBType.Numeric, DBUtilDirection.In, 18, match.Fees);
                oDBUtility.AddParameters("@team1_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, match.Team1ID);
                oDBUtility.AddParameters("@team2_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, match.Team2ID);
                oDBUtility.AddParameters("@winner_team_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, match.WinnerTeamID);
                oDBUtility.AddParameters("@modified_date", DBUtilDBType.DateTime, DBUtilDirection.In, 10, DateTime.Now);
                oDBUtility.AddParameters("@modified_by", DBUtilDBType.Integer, DBUtilDirection.In, 10, match.ModifiedBy);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_UPDATE_MATCH");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }
    }
}
