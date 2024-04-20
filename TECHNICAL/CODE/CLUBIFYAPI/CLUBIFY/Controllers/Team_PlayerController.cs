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
    public class Team_PlayerController : Controller
    {
        private readonly IMSConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public Team_PlayerController(IMSConfigManager configuration, IWebHostEnvironment env)
        {
            this._configurationIG = configuration;
            this._env = env;
        }

        [Route("Team_Player/Get")]
        [HttpPost]
        public IActionResult Get(TeamPlayer teamPlayer)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);

                if (teamPlayer.TeamPlayerID != 0)
                {
                    oDBUtility.AddParameters("@team_player_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, teamPlayer.TeamPlayerID);
                }

                if (teamPlayer.TeamID != 0)
                {
                    oDBUtility.AddParameters("@team_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, teamPlayer.TeamID);
                }

                if (teamPlayer.PlayerID != 0)
                {
                    oDBUtility.AddParameters("@player_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, teamPlayer.PlayerID);
                }

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_GET_TEAM_PLAYER");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Team_Player/Insert")]
        [HttpPost]
        public IActionResult Post(TeamPlayer teamPlayer)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@team_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, teamPlayer.TeamID);
                oDBUtility.AddParameters("@player_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, teamPlayer.PlayerID);
                oDBUtility.AddParameters("@created_date", DBUtilDBType.DateTime, DBUtilDirection.In, 10, DateTime.Now);
                oDBUtility.AddParameters("@created_by", DBUtilDBType.Integer, DBUtilDirection.In, 10, teamPlayer.CreatedBy);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_INSERT_TEAM_PLAYER");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Team_Player/Update")]
        [HttpPost]
        public IActionResult Put(TeamPlayer teamPlayer)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@team_player_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, teamPlayer.TeamPlayerID);
                oDBUtility.AddParameters("@team_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, teamPlayer.TeamID);
                oDBUtility.AddParameters("@player_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, teamPlayer.PlayerID);
                oDBUtility.AddParameters("@modified_date", DBUtilDBType.DateTime, DBUtilDirection.In, 10, DateTime.Now);
                oDBUtility.AddParameters("@modified_by", DBUtilDBType.Integer, DBUtilDirection.In, 10, teamPlayer.ModifiedBy);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_UPDATE_TEAM_PLAYER");
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
