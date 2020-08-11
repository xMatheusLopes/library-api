using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using library_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_api.Controllers
{
    public class UploadController : Controller
    {
        [Route("upload/{type}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UploadImageAsync(IFormFile file, String type)
        {
            UploadService us = new UploadService();
            var filePath = await us.Upload(file, type);
            if (filePath != null) 
                return Ok(filePath);

            return StatusCode(500, "Não foi possível realizar esta ação");
        }
    }
}