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
    public class GenderController : Controller
    {
        private readonly IMSConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public GenderController(IMSConfigManager configuration, IWebHostEnvironment env)
        {
            this._configurationIG = configuration;
            this._env = env;
        }

        [Route("Gender/Get")]
        [HttpPost]
        public IActionResult Get(Gender gender)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);

                if (gender.GenderID != 0)
                {
                    oDBUtility.AddParameters("@gender_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, gender.GenderID);
                }

                if (!string.IsNullOrEmpty(gender.GenderName))
                {
                    oDBUtility.AddParameters("@gender_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, gender.GenderName);
                }

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_GET_GENDER");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Gender/Insert")]
        [HttpPost]
        public IActionResult Post(Gender gender)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@gender_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, gender.GenderName);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_INSERT_GENDER");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Gender/Update")]
        [HttpPost]
        public IActionResult Put(Gender gender)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@gender_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, gender.GenderID);
                oDBUtility.AddParameters("@gender_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, gender.GenderName);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_UPDATE_GENDER");
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
