using Clubify.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using MSConfiguration;
using Microsoft.AspNetCore.Authorization;

namespace Clubify.Controllers
{
    [Authorize]
    [ApiController]
    public class UploadController : Controller
    {
        private readonly IMSConfigManager _configurationIG;
        private readonly IWebHostEnvironment _env;
        private ServiceRequestProcessor oServiceRequestProcessor;
      
        public UploadController(IMSConfigManager configuration, IWebHostEnvironment env)
        {
            this._configurationIG = configuration;
            this._env = env;
        }

        [Route("Upload")]
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 504857600)]
        [DisableRequestSizeLimit]
        public IActionResult MediaUpload()
        {

            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;

                string path = postedFile.Name; //Name means path 
                var physicalPath = _env.ContentRootPath + "/Images/" + path +"/" + filename;

                if (!System.IO.Directory.Exists(_env.ContentRootPath + "/Images/" + path + "/"))
                {
                    System.IO.Directory.CreateDirectory(_env.ContentRootPath + "/Images/" + path + "/");
                }

                if (System.IO.File.Exists(physicalPath))
                {
                    System.IO.File.Delete(physicalPath);
                }

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return Ok();

            }
            catch (Exception ex)
            {
                oServiceRequestProcessor = new ServiceRequestProcessor();
                return BadRequest(oServiceRequestProcessor.onError(ex.Message));
            }
        }
    }
}
