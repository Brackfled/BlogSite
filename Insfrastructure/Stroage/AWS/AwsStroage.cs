using Amazon.S3;
using Amazon.S3.Model;
using Core.CrossCuttingConserns.Exceptions.Types;
using Infrastructure.Stroage;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insfrastructure.Stroage.AWS
{
    public class AwsStroage: IStroage
    {
        private readonly IAmazonS3 _amazonS3Client;

        public AwsStroage(IAmazonS3 amazonS3)
        {
            _amazonS3Client = amazonS3;
        }

        public async Task<string> CreateBucketAsync(string bucketName)
        {
            var bucketExists = await Amazon.S3.Util.AmazonS3Util.DoesS3BucketExistV2Async(_amazonS3Client, bucketName);
            if (bucketExists)
                throw new BusinessException("Bucket Zaten Mevcut");

            await _amazonS3Client.PutBucketAsync(bucketName);
            return $"Created Bucket ==> {bucketName}";
        }

        public async Task<ListBucketsResponse> GetListBucketsAsync()
        {
            
            ListBucketsResponse bucketDatas = await _amazonS3Client.ListBucketsAsync();
            return bucketDatas;
        }

        public async Task<string> DeleteBucketAsync(string bucketName)
        {
            await _amazonS3Client.DeleteBucketAsync(bucketName);
            return $"Deleted Bucket ==> {bucketName}";
        }

        public async Task<(string fileName, string bucketName, string fileUrl)> UploadFileAsync(IFormFile formFile, string bucketName)
        {
            bool bucketExists = await _amazonS3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists)
                throw new BusinessException("Bucket Bulunamadı");

            string newFileName = await GetUniqeFileNameAsync(bucketName, formFile.FileName);

            PutObjectRequest request = new()
            {
                BucketName = bucketName,
                Key = newFileName,
                InputStream = formFile.OpenReadStream(),
                CannedACL = S3CannedACL.PublicRead
            };
           
            request.Metadata.Add("Content-Type", formFile.ContentType);
            await _amazonS3Client.PutObjectAsync(request);

            GetPreSignedUrlRequest preSignedUrlRequest = new()
            {
                BucketName = request.BucketName,
                Key = request.Key,
                Expires = DateTime.UtcNow.AddMonths(9)
            };

            string fileUrl = await _amazonS3Client.GetPreSignedURLAsync(preSignedUrlRequest);

            //string fileUrl = $"https://{bucketName}.s3.amazonaws.com/{request.Key}";

            return (request.Key, request.BucketName, fileUrl);
        }

        public async Task<List<S3ObjectDto>> GetListFilesAsync(string bucketName)
        {
            bool bucketExists = await _amazonS3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists)
                throw new BusinessException("Bucket Bulunamadı");

            ListObjectsV2Request request = new()
            {
                BucketName = bucketName,
            };

            ListObjectsV2Response response = await _amazonS3Client.ListObjectsV2Async(request);
            List<S3ObjectDto> objectDatas = response.S3Objects.Select(@object =>
            {
                GetPreSignedUrlRequest urlRequest = new()
                {
                    BucketName = bucketName,
                    Key = @object.Key,
                    Expires = DateTime.UtcNow.AddMonths(9)
                };

                return new S3ObjectDto
                {
                    Name = @object.Key,
                    Path = @object.BucketName,
                    Url = _amazonS3Client.GetPreSignedURL(urlRequest),
                };
            }).ToList();

            return objectDatas;
        }

        public async Task<DeleteObjectResponse> DeleteFileAsync(string bucketName, string fileName)
        {
            bool bucketExists = await _amazonS3Client.DoesS3BucketExistAsync(bucketName);
            if (!bucketExists)
                throw new BusinessException("Bucket Bulunamadı");

            DeleteObjectResponse deletedS3Object =  await _amazonS3Client.DeleteObjectAsync(bucketName, fileName);
            return deletedS3Object;
        }

        private async Task<string> GetUniqeFileNameAsync(string bucketName, string key)
        {
            bool fileNameState = await DoesObjectExistsAsync(bucketName, key);

            if (!fileNameState)
            {
                return key;
            } else
            {
                int counter = 1;
                string baseFileName = key.Substring(0, key.LastIndexOf('.'));
                string extesion = key.Substring(key.LastIndexOf('.'));
                string uniqeFileName;
                bool fileStateAfter;

                do
                {
                    fileStateAfter = false;
                    uniqeFileName = $"{baseFileName}-{counter++}{extesion}";
                    bool uniqeFileNameState = await DoesObjectExistsAsync(bucketName, uniqeFileName);
                    if (uniqeFileNameState== true)
                        fileStateAfter = true;

                } while (fileStateAfter == true);

                return uniqeFileName;
            }
        }

        private async Task<bool> DoesObjectExistsAsync(string bucketName, string key)
        {
            try
            {
                GetObjectResponse response = await _amazonS3Client.GetObjectAsync(bucketName, key);
                return true;
            }catch (Exception ex)
            {
                return false;
            }
          
        }
    }
}
