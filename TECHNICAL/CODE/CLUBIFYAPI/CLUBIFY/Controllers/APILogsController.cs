using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using DAE.DAL.SQL;
using MSConfiguration;
using System;
using Clubify.Models;
using Microsoft.AspNetCore.Authorization;

namespace Clubify.Controllers
{
    [Authorize]
    [ApiController]
    public class APILogsController : ControllerBase
    {
        private readonly IMSConfigManager _configurationIG;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public APILogsController(IMSConfigManager configuration)
        {
            _configurationIG = configuration;
        }

        [Route("APILogs/get")]
        [HttpPost]
        public IActionResult Get(Clubify.Models.APILogs aPILogs)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);

             
                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("USP_GET_City");
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
