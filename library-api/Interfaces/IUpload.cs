using System.Collections;
using System.Threading.Tasks;
using library_api.Entities;
using Microsoft.AspNetCore.Http;

namespace library_api.Interfaces {
    public interface IUpload
    {
        ArrayList GetUploadTypes();

        Task<string> Upload(IFormFile file, string type);
    }
}