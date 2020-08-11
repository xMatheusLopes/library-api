using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using library_api.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace library_api.Controllers
{
    public class UploadController : Controller
    {        
        private readonly IUpload _uploadService;

        public UploadController(IUpload uploadService)
        {
            _uploadService = uploadService;
        }

        [Route("upload/{type}")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UploadImageAsync(IFormFile file, String type)
        {
            var filePath = await _uploadService.Upload(file, type);
            if (filePath != null) 
                return Ok(filePath);

            return StatusCode(500, "Não foi possível realizar esta ação");
        }
    }
}