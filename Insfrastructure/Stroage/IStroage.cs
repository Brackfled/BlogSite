using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Stroage
{
    public interface IStroage
    {

        Task<(string fileName, string pathOrContainerName)> UploadAsync(string pathOrContainerName, IFormFile files);
        Task DeleteAsync(string pathOrContainerName, string fileName);
        List<string> GetFiles(string pathOrContainerName);
        bool HasFile(string pathOrContainerName, string fileName);
        Task<string> GetFileUrl(string fileName, string pathOrContainerName);

    }
}
