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
    public class PlayerController : Controller
    {
        private readonly IMSConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public PlayerController(IMSConfigManager configuration, IWebHostEnvironment env)
        {
            this._configurationIG = configuration;
            this._env = env;
        }

        [Route("Player/Get")]
        [HttpPost]
        public IActionResult Get(Player player)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);

                if (player.PlayerID != 0)
                {
                    oDBUtility.AddParameters("@player_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, player.PlayerID);
                }

                if (!string.IsNullOrEmpty(player.FirstName))
                {
                    oDBUtility.AddParameters("@first_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, player.FirstName);
                }

                if (!string.IsNullOrEmpty(player.LastName))
                {
                    oDBUtility.AddParameters("@last_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, player.LastName);
                }

                if (!string.IsNullOrEmpty(player.Email))
                {
                    oDBUtility.AddParameters("@email", DBUtilDBType.Varchar, DBUtilDirection.In, 100, player.Email);
                }

                if (player.DateOfBirth != null)
                {
                    oDBUtility.AddParameters("@date_of_birth", DBUtilDBType.Date, DBUtilDirection.In, 10, player.DateOfBirth);
                }

                if (!string.IsNullOrEmpty(player.ProfilePic))
                {
                    oDBUtility.AddParameters("@profile_pic", DBUtilDBType.Nvarchar, DBUtilDirection.In, 500, player.ProfilePic);
                }

                if (player.RoleID != 0)
                {
                    oDBUtility.AddParameters("@role_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, player.RoleID);
                }

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_GET_PLAYER");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Player/Insert")]
        [HttpPost]
        public IActionResult Post(Player player)
        {
            try
            {
                ConfigHandler oEncrDec;
                oEncrDec = new ConfigHandler(this._configurationIG.EncryptionDecryptionAlgorithm, this._configurationIG.EncryptionDecryptionKey);

                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@first_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, player.FirstName);
                oDBUtility.AddParameters("@last_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, player.LastName);
                oDBUtility.AddParameters("@email", DBUtilDBType.Varchar, DBUtilDirection.In, 100, player.Email);
                oDBUtility.AddParameters("@date_of_birth", DBUtilDBType.Date, DBUtilDirection.In, 10, player.DateOfBirth);
                oDBUtility.AddParameters("@profile_pic", DBUtilDBType.Nvarchar, DBUtilDirection.In, 500, player.ProfilePic);
                oDBUtility.AddParameters("@role_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, player.RoleID);

                string decryptedPassword = oEncrDec.Cryptohelper.Encrypt(player.Password);

                oDBUtility.AddParameters("@password", DBUtilDBType.Varchar, DBUtilDirection.In, 500, decryptedPassword);
                oDBUtility.AddParameters("@is_active", DBUtilDBType.Boolean, DBUtilDirection.In, 1, player.IsActive);
                oDBUtility.AddParameters("@created_date", DBUtilDBType.DateTime, DBUtilDirection.In, 10, DateTime.Now);
                oDBUtility.AddParameters("@created_by", DBUtilDBType.Integer, DBUtilDirection.In, 10, player.CreatedBy);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_INSERT_PLAYER");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Player/Update")]
        [HttpPost]
        public IActionResult Put(Player player)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@player_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, player.PlayerID);
                oDBUtility.AddParameters("@first_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, player.FirstName);
                oDBUtility.AddParameters("@last_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, player.LastName);
                oDBUtility.AddParameters("@email", DBUtilDBType.Varchar, DBUtilDirection.In, 100, player.Email);
                oDBUtility.AddParameters("@date_of_birth", DBUtilDBType.Date, DBUtilDirection.In, 10, player.DateOfBirth);
                oDBUtility.AddParameters("@profile_pic", DBUtilDBType.Nvarchar, DBUtilDirection.In, 500, player.ProfilePic);
                oDBUtility.AddParameters("@role_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, player.RoleID);
                oDBUtility.AddParameters("@password", DBUtilDBType.Varchar, DBUtilDirection.In, 50, player.Password);
                oDBUtility.AddParameters("@is_active", DBUtilDBType.Boolean, DBUtilDirection.In, 1, player.IsActive);
                oDBUtility.AddParameters("@modified_date", DBUtilDBType.DateTime, DBUtilDirection.In, 10, DateTime.Now);
                oDBUtility.AddParameters("@modified_by", DBUtilDBType.Integer, DBUtilDirection.In, 10, player.ModifiedBy);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_UPDATE_PLAYER");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }




        [Route("Player/Registration")]
        [HttpPost]
        public IActionResult Registration(Player player)
        {
            try
            {
                ConfigHandler oEncrDec;
                oEncrDec = new ConfigHandler(this._configurationIG.EncryptionDecryptionAlgorithm, this._configurationIG.EncryptionDecryptionKey);

                DBUtility oDBUtility = new DBUtility(_configurationIG);
                oDBUtility.AddParameters("@first_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, player.FirstName);
                oDBUtility.AddParameters("@last_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, player.LastName);
                oDBUtility.AddParameters("@email", DBUtilDBType.Varchar, DBUtilDirection.In, 100, player.Email);
                oDBUtility.AddParameters("@date_of_birth", DBUtilDBType.Date, DBUtilDirection.In, 10, player.DateOfBirth);
                oDBUtility.AddParameters("@profile_pic", DBUtilDBType.Nvarchar, DBUtilDirection.In, 500, "default.png");
                oDBUtility.AddParameters("@role_id", DBUtilDBType.Integer, DBUtilDirection.In, 10, 3);

                string decryptedPassword = oEncrDec.Cryptohelper.Encrypt(player.Password);

                oDBUtility.AddParameters("@password", DBUtilDBType.Varchar, DBUtilDirection.In, 500, decryptedPassword);
                oDBUtility.AddParameters("@is_active", DBUtilDBType.Boolean, DBUtilDirection.In, 1, false);
                oDBUtility.AddParameters("@created_date", DBUtilDBType.DateTime, DBUtilDirection.In, 10, DateTime.Now);
                oDBUtility.AddParameters("@created_by", DBUtilDBType.Integer, DBUtilDirection.In, 10,1 );
                oDBUtility.AddParameters("@is_approved", DBUtilDBType.Boolean, DBUtilDirection.In, 1, false);
                oDBUtility.AddParameters("@membership_start_date", DBUtilDBType.DateTime, DBUtilDirection.In, 10, DateTime.Now);
                DateTime currentDate = DateTime.Now;
                DateTime membershipEndDate = currentDate.AddYears(1);
                oDBUtility.AddParameters("@membership_end_date", DBUtilDBType.DateTime, DBUtilDirection.In, 10, membershipEndDate);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_INSERT_PLAYER");
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
