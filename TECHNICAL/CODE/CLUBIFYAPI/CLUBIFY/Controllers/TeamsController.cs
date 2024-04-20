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
    public class TeamsController : Controller
    {
        private readonly IMSConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public TeamsController(IMSConfigManager configuration, IWebHostEnvironment env)
        {
            this._configurationIG = configuration;
            this._env = env;
        }

        [Route("Teams/Get")]
        [HttpPost]
        public IActionResult Get(Team team)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);

                if (team.TeamID != 0)
                {
                    oDBUtility.AddParameters("@team_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, team.TeamID);
                }

                if (!string.IsNullOrEmpty(team.TeamName))
                {
                    oDBUtility.AddParameters("@team_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, team.TeamName);
                }

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_GET_TEAMS");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Teams/Insert")]
        [HttpPost]
        public IActionResult Post(Team team)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@team_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, team.TeamName);
                oDBUtility.AddParameters("@captain_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, team.CaptainID);
                oDBUtility.AddParameters("@logo", DBUtilDBType.Nvarchar, DBUtilDirection.In, 500, team.Logo);
                oDBUtility.AddParameters("@description", DBUtilDBType.Varchar, DBUtilDirection.In, 500, team.Description);
                oDBUtility.AddParameters("@created_date", DBUtilDBType.DateTime, DBUtilDirection.In, 10, DateTime.Now);
                oDBUtility.AddParameters("@created_by", DBUtilDBType.Integer, DBUtilDirection.In, 10, team.CreatedBy);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_INSERT_TEAMS");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Teams/Update")]
        [HttpPost]
        public IActionResult Put(Team team)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@team_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, team.TeamID);
                oDBUtility.AddParameters("@team_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, team.TeamName);
                oDBUtility.AddParameters("@captain_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, team.CaptainID);
                oDBUtility.AddParameters("@logo", DBUtilDBType.Nvarchar, DBUtilDirection.In, 500, team.Logo);
                oDBUtility.AddParameters("@description", DBUtilDBType.Varchar, DBUtilDirection.In, 500, team.Description);
                oDBUtility.AddParameters("@modified_date", DBUtilDBType.DateTime, DBUtilDirection.In, 10, DateTime.Now);
                oDBUtility.AddParameters("@modified_by", DBUtilDBType.Integer, DBUtilDirection.In, 10, team.ModifiedBy);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_UPDATE_TEAMS");
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
