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
    public class SettingsController : Controller
    {
        private readonly IMSConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;

        public SettingsController(IMSConfigManager configuration, IWebHostEnvironment env)
        {
            this._configurationIG = configuration;
            this._env = env;
        }

        [Route("Settings/Get")]
        [HttpPost]
        public IActionResult Get()
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);

                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_GET_SETTINGS");
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return Ok(oServiceRequestProcessor.ProcessRequest(ds));
            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }

        [Route("Settings/Update")]
        [HttpPost]
        public IActionResult Put(Settings settings)
        {
            try
            {
                DBUtility oDBUtility = new DBUtility(_configurationIG);
                if (settings.ClubName != null)
                    oDBUtility.AddParameters("@club_name", DBUtilDBType.Varchar, DBUtilDirection.In, 50, settings.ClubName);

                if (settings.Logo != null)
                    oDBUtility.AddParameters("@logo", DBUtilDBType.Nvarchar, DBUtilDirection.In, 500, settings.Logo);

                if (settings.Email != null)
                    oDBUtility.AddParameters("@email", DBUtilDBType.Varchar, DBUtilDirection.In, 100, settings.Email);

                if (settings.Number != null)
                    oDBUtility.AddParameters("@number", DBUtilDBType.Varchar, DBUtilDirection.In, 50, settings.Number);

                if (settings.AboutUs != null)
                    oDBUtility.AddParameters("@about_us", DBUtilDBType.Nvarchar, DBUtilDirection.In, -1, settings.AboutUs);

                if (settings.SmtpServerAddress != null)
                    oDBUtility.AddParameters("@smtp_server_address", DBUtilDBType.Nvarchar, DBUtilDirection.In, 255, settings.SmtpServerAddress);

                if (settings.SmtpPort != 0)
                    oDBUtility.AddParameters("@smtp_port", DBUtilDBType.Integer, DBUtilDirection.In, 20, settings.SmtpPort);

                if (settings.SmtpUsername != null)
                    oDBUtility.AddParameters("@smtp_username", DBUtilDBType.Nvarchar, DBUtilDirection.In, 100, settings.SmtpUsername);

                if (settings.SmtpPassword != null)
                    oDBUtility.AddParameters("@smtp_password", DBUtilDBType.Nvarchar, DBUtilDirection.In, 100, settings.SmtpPassword);

                if (settings.SenderEmail != null)
                    oDBUtility.AddParameters("@sender_email", DBUtilDBType.Nvarchar, DBUtilDirection.In, 100, settings.SenderEmail);

                if (settings.SenderName != null)
                    oDBUtility.AddParameters("@sender_name", DBUtilDBType.Nvarchar, DBUtilDirection.In, 100, settings.SenderName);

                if (settings.UseSslTls != null)
                    oDBUtility.AddParameters("@use_ssl_tls", DBUtilDBType.Boolean, DBUtilDirection.In, 0, settings.UseSslTls);

                if (settings.DefaultEmailSubjectPrefix != null)
                    oDBUtility.AddParameters("@default_email_subject_prefix", DBUtilDBType.Nvarchar, DBUtilDirection.In, 100, settings.DefaultEmailSubjectPrefix);

                if (settings.DefaultEmailBody != null)
                    oDBUtility.AddParameters("@default_email_body", DBUtilDBType.Nvarchar, DBUtilDirection.In, -1, settings.DefaultEmailBody);

                if (settings.EmailFooter != null)
                    oDBUtility.AddParameters("@email_footer", DBUtilDBType.Nvarchar, DBUtilDirection.In, -1, settings.EmailFooter);

                if (settings.DefaultCcEmail != null)
                    oDBUtility.AddParameters("@default_cc_email", DBUtilDBType.Nvarchar, DBUtilDirection.In, 100, settings.DefaultCcEmail);

                if (settings.DefaultBccEmail != null)
                    oDBUtility.AddParameters("@default_bcc_email", DBUtilDBType.Nvarchar, DBUtilDirection.In, 100, settings.DefaultBccEmail);

                if (settings.EmailSendingLimitPerHour != 0)
                    oDBUtility.AddParameters("@email_sending_limit_per_hour", DBUtilDBType.Integer, DBUtilDirection.In, 0, settings.EmailSendingLimitPerHour);


                DataSet ds = oDBUtility.Execute_StoreProc_DataSet("MSP_UPDATE_SETTINGS");
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
