using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Core.CrossCuttingConserns.Exceptions.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Stroage.Azure
{
    public class AzureStroage //:IStroage
    {
        //public IConfiguration Configuration { get; }
        //private readonly BlobServiceClient _blobServiceClient;
        //BlobContainerClient _blobContainerClient;

        //public AzureStroage(IConfiguration configuration)
        //{


        //    _blobServiceClient = new(configuration["Azure"]);
        //}

        //public async Task DeleteAsync(string pathOrContainerName, string fileName)
        //{
        //    _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
        //    BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        //    await blobClient.DeleteAsync();

        //}

        //public async Task<string> GetFileUrl(string fileName, string pathOrContainerName)
        //{
        //    _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
        //    BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
        //    if (await blobClient.ExistsAsync())
        //    {
        //        return blobClient.Uri.ToString();
        //    } else
        //    {
        //        throw new BusinessException("Dosya Mevcut Değil (AzurStroage)");
        //    }
        //}

        //public List<string> GetFiles(string pathOrContainerName)
        //{
        //    _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
        //    return _blobContainerClient.GetBlobs().Select(f => f.Name).ToList();
        //}

        //public bool HasFile(string pathOrContainerName, string fileName)
        //{
        //    _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
        //    return _blobContainerClient.GetBlobs().Any(f => f.Name == fileName);
        //}

        //public async Task<(string fileName, string pathOrContainerName)> UploadAsync(string pathOrContainerName, IFormFile formFile)
        //{
        //    _blobContainerClient = _blobServiceClient.GetBlobContainerClient(pathOrContainerName);
        //    await _blobContainerClient.CreateIfNotExistsAsync();
        //    await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

        //    string uniqueFileName = await GetUniqueBlobNameAsync(formFile.FileName, pathOrContainerName);

        //    BlobClient blobClient = _blobContainerClient.GetBlobClient(uniqueFileName);

        //    await blobClient.UploadAsync(formFile.OpenReadStream());

        //    return (uniqueFileName, pathOrContainerName);
            
        //}

        //public async Task<string> GetUniqueBlobNameAsync( string blobName, string pathOrContainerName)
        //{
        //    bool fileStateBefore = HasFile(pathOrContainerName, blobName);

        //    if (fileStateBefore == false)
        //    {
        //        // Dosya adı eşsizse, aynı adı döndür.
        //        return blobName;
        //    }
        //    else
        //    {
        //        // Dosya adı eşsiz değilse, dosya adına rakam ekleyerek eşsiz bir ad oluştur.
        //        int counter = 1;
        //        string baseBlobName = blobName.Substring(0, blobName.LastIndexOf('.'));
        //        string extension = blobName.Substring(blobName.LastIndexOf('.'));
        //        string uniqueBlobName;
        //        bool fileStateAfter;

        //        do
        //        {
        //            fileStateAfter = false;
        //            uniqueBlobName = $"{baseBlobName}-{counter++}{extension}";
        //            if(HasFile(pathOrContainerName, uniqueBlobName) == true)
        //                fileStateAfter = true;
        //        } while (fileStateAfter == true);

        //        return uniqueBlobName;
        //    }
        //}
    }
}
