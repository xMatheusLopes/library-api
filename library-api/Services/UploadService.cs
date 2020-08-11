using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using library_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace library_api.Services
{
    public class UploadService : IUpload {
        private readonly IConfiguration _config;

        public UploadService(IConfiguration config) {
            _config = config;
        }
        public ArrayList GetUploadTypes() {
            ArrayList list = new ArrayList();
            list.Add("img");
            list.Add("doc");
            return list;
        }

        public async Task<string> Upload(IFormFile file, string type) {
            ArrayList types = GetUploadTypes();
            
            if (types.Contains(type) && file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/storage/" + type, fileName.ToString() + Path.GetExtension(file.FileName));
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return _config.GetValue<String>("baseURL") + "storage/" + type + "/" + fileName.ToString() + Path.GetExtension(file.FileName);
            }
            return null;
        }
    }
}
