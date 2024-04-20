using Clubify.Models;
using DAE.DAL.SQL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System;
using MSConfiguration;
using DAE.Common.EncryptionDecryption;

namespace Clubify.Controllers
{
    //[Authorize]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IMSConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;
        private readonly IJwtAuth jwtAuth;

        public LoginController(IMSConfigManager configuration, IWebHostEnvironment env, IJwtAuth jwtAuth)
        {
            this._configurationIG = configuration;
            this._env = env;
            this.jwtAuth = jwtAuth; 
        }


        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public IActionResult Login(Player player)
        {
            try
            {
                if(player.Email == null)
                    return Ok(oServiceRequestProcessor.onUserNotFound());

                ConfigHandler oEncrDec;
                oEncrDec = new ConfigHandler(this._configurationIG.EncryptionDecryptionAlgorithm, this._configurationIG.EncryptionDecryptionKey);

                DBUtility oDBUtility = new DBUtility(_configurationIG);

                oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@email", DBUtilDBType.Varchar, DBUtilDirection.In, 50, player.Email);                
                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_GET_LOGIN");

                int status_code = Convert.ToInt32(ds.Tables[0].Rows[0]["status_code"].ToString());

                if (status_code == 100)
                {
                    string decryptedPassword = oEncrDec.Cryptohelper.Decrypt(ds.Tables[0].Rows[0]["password"].ToString());

                    if (player.Password == decryptedPassword)
                    {
                        oServiceRequestProcessor = new ServiceRequestProcessor();

                        var token = jwtAuth.Authentication();

                        System.Data.DataColumn newColumn = new System.Data.DataColumn("Token", typeof(System.String));
                        newColumn.DefaultValue = token.ToString();
                        ds.Tables[0].Columns.Add(newColumn);
                        ds.Tables[0].Columns.Remove("password");

                        return Ok(oServiceRequestProcessor.ProcessRequest(ds));
                    }
                    else
                    {
                        oServiceRequestProcessor = new ServiceRequestProcessor();
                        return Ok(oServiceRequestProcessor.onUserNotFound());
                    }
                }
                else
                {
                    oServiceRequestProcessor = new ServiceRequestProcessor();
                    return Ok(oServiceRequestProcessor.ProcessRequest(ds));
                }
            }
            catch(Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

    }
}
