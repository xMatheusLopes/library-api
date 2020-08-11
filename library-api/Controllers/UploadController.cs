using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using library_api.Services;
using library_api.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
    public class UploadController : Controller
    {
        private static Global _global;

        public UploadController(Global global)
        {
            _global = global;
        }

        [Route("upload/{type}")]
        [HttpGet]
        // [Authorize]
        public async Task<IActionResult> UploadImageAsync(IFormFile file, String type)
        {
            UploadService us = new UploadService();
            var filePath = await us.Upload(file, type, _global.BaseUrl);
            if (filePath != null) 
                return Ok(filePath);

            return StatusCode(500, "Não foi possível realizar esta ação");
        }
    }
}