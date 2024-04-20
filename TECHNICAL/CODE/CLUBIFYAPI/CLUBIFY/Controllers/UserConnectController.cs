using Microsoft.AspNetCore.Mvc;
using DAE.DAL.SQL;
using MSConfiguration;
using System;
using System.Data;
using Clubify.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace Clubify.Controllers
{
    [Authorize]
    [ApiController]
    public class UserConnectController : ControllerBase
    {
        private readonly IMSConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public UserConnectController(IMSConfigManager configuration, IWebHostEnvironment env)
        {
            _configurationIG = configuration;
            _env = env;
        }

        [Route("UserConnect/Get")]
        [HttpPost]
        public IActionResult Get(UserConnect userConnect)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);

                if (userConnect.ID != 0)
                    oDBUtility.AddParameters("@ID", DBUtilDBType.Integer, DBUtilDirection.In, 10, userConnect.ID);

                if (userConnect.UserID != 0)
                    oDBUtility.AddParameters("@UserID", DBUtilDBType.Integer, DBUtilDirection.In, 10, userConnect.UserID);

                if (userConnect.ConnectedUserID != 0)
                    oDBUtility.AddParameters("@ConnectedUserID", DBUtilDBType.Integer, DBUtilDirection.In, 10, userConnect.ConnectedUserID);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_GET_USERCONNECT");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("UserConnect/Insert")]
        [HttpPost]
        public IActionResult Post(UserConnect userConnect)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@UserID", DBUtilDBType.Integer, DBUtilDirection.In, 10, userConnect.UserID);
                oDBUtility.AddParameters("@ConnectedUserID", DBUtilDBType.Integer, DBUtilDirection.In, 10, userConnect.ConnectedUserID);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_INSERT_USERCONNECT");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("UserConnect/Update")]
        [HttpPost]
        public IActionResult Put(UserConnect userConnect)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);

                oDBUtility.AddParameters("@ID", DBUtilDBType.Integer, DBUtilDirection.In, 10, userConnect.ID);
                oDBUtility.AddParameters("@UserID", DBUtilDBType.Integer, DBUtilDirection.In, 10, userConnect.UserID);
                oDBUtility.AddParameters("@ConnectedUserID", DBUtilDBType.Integer, DBUtilDirection.In, 10, userConnect.ConnectedUserID);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_UPDATE_USERCONNECT");
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
