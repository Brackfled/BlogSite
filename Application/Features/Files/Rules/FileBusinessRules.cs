using Core.Application.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Rules
{
    public class FileBusinessRules: BaseBusinessRules
    {
        public Task FileIsImageFile(string fileExtension)
        {
            string[] extensionList = { ".gif", ".png", ".jpg", ".jpeg" };

            bool isSuccess = extensionList.Any(extension => extension.Contains(fileExtension));
            if (isSuccess)
                return Task.CompletedTask;

            throw new Exception("Dosya Image Dosyası Değil!");
        }
    }
}
