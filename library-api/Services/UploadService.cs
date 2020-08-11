using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace library_api.Services
{
    public class UploadService {
        public ArrayList GetUploadTypes() {
            ArrayList list = new ArrayList();
            list.Add("img");
            list.Add("doc");
            return list;
        }

        public async Task<string> Upload(IFormFile file, string type, string baseUrl) {
            ArrayList types = GetUploadTypes();
            
            if (types.Contains(type) && file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid();
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/storage/" + type, fileName.ToString() + Path.GetExtension(file.FileName));
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return baseUrl + "storage/" + type + "/" + fileName.ToString() + Path.GetExtension(file.FileName);
            }
            return null;
        }
    }
}
