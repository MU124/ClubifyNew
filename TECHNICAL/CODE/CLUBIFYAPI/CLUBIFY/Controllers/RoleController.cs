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
    public class RoleController : Controller
    {
        private readonly IMSConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public RoleController(IMSConfigManager configuration, IWebHostEnvironment env)
        {
            this._configurationIG = configuration;
            this._env = env;
        }

        [Route("Role/Get")]
        [HttpPost]
        public IActionResult Get(Role role)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);

                if (role.RoleID != 0)
                {
                    oDBUtility.AddParameters("@role_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, role.RoleID);
                }

                if (!string.IsNullOrEmpty(role.RoleName))
                {
                    oDBUtility.AddParameters("@role_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, role.RoleName);
                }

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_GET_ROLE");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Role/Insert")]
        [HttpPost]
        public IActionResult Post(Role role)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@role_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, role.RoleName);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_INSERT_ROLE");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Role/Update")]
        [HttpPost]
        public IActionResult Put(Role role)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@role_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, role.RoleID);
                oDBUtility.AddParameters("@role_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, role.RoleName);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_UPDATE_ROLE");
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
