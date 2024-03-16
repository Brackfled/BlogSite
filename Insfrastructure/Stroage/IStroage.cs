using Amazon.S3.Model;
using Insfrastructure.Stroage.AWS;
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

        Task<string> CreateBucketAsync(string bucketName);
        Task<ListBucketsResponse> GetListBucketsAsync();
        Task<string> DeleteBucketAsync(string bucketName);
        Task<(string fileName, string bucketName, string fileUrl)> UploadFileAsync(IFormFile formFile, string bucketName);
        Task<List<S3ObjectDto>> GetListFilesAsync(string bucketName);
        Task<DeleteObjectResponse> DeleteFileAsync(string bucketName, string fileName);

    }
}
