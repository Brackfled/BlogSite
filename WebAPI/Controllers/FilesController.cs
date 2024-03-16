using Amazon.S3.Model;
using Amazon.S3;
using Application.Features.Files.Commands.CreatePPFile;
using Core.CrossCuttingConserns.Exceptions.Types;
using Insfrastructure.Stroage.AWS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Core.Application.Response;
using Application.Features.Files.Queries.GetListPPFile;
using Application.Features.Files.Commands.DeletePPFile;
using Application.Features.Files.Queries.GetByIdPPFile;
using Application.Features.Files.Commands.CreateSubjectImageFile;
using Application.Features.Files.Commands.DeleteSubjectImageFile;
using Application.Features.Files.Queries.GetListSubjectImageFile;
using Application.Features.Files.Queries.GetByIdSubjectImageFile;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : BaseController
    {
        private readonly IAmazonS3 _amazonS3Client;

        public FilesController(IAmazonS3 amazonS3Client)
        {
            _amazonS3Client = amazonS3Client;
        }

        // =====================> PPFile ile ilgili endpointler <=====================

        [HttpPost("UploadPPFile")]
        public async Task<IActionResult> UploadPPFile(string bucketName, IFormFile formFile)
        {
            CreatePPFileCommand command = new() { BucketName = bucketName, FormFile = formFile, UserId = getUserIdFromRequest() };
            CreatedPPFileResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("GetListPPFile")]
        public async Task<IActionResult> GetListPPFile()
        {
            GetListResponse<GetListPPFileListItemDto> response = await Mediator.Send(new GetListPPFileQuery());
            return Ok(response);
        }

        [HttpGet("GetByIdPPFile")]
        public async Task<IActionResult> GetByIdPPFile([FromBody] Guid id)
        {
            GetByIdPPFileQuery query = new() { Id = id };
            GetByIdPPFileDto response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpDelete("DeletePPFile")]
        public async Task<IActionResult> DeletePPFile([FromBody] Guid id)
        {
            DeletePPFileCommand command = new() { Id = id };
            DeletedPPFileResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery]string bucketName)
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
                    Expires = DateTime.UtcNow.AddMinutes(1)
                };

                return new S3ObjectDto
                {
                    Name = @object.Key,
                    Url = _amazonS3Client.GetPreSignedURL(urlRequest),
                };
            }).ToList();

            return Ok(objectDatas);
        }

        // ------------------- PPFile ile ilgili endpointlerin sonu ---------------------


        // =====================> SubjectImageFile ile ilgili endpointler <=====================

        [HttpPost("UploadSubjectImageFile")]
        public async Task<IActionResult> UploadSubjectImageFile(Guid id, IFormFile formFile)
        {
            CreateSubjectImageFileDto dto = new() { SubjectId = id , BucketName ="flepix-blog-subjectfiles"};
            CreateSubjectImageFileCommand command = new() { CreateSubjectImageFileDto = dto, FormFile = formFile};
            CreatedSubjectImageFileResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("DeleteSubjectImageFile/{id}")]
        public async Task<IActionResult> DeleteSubjectImageFile([FromRoute] Guid id)
        {
            DeleteSubjectImageFileCommand command = new() { Id = id };
            DeletedSubjectImageFileResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("GetListSubjectImageFiles")]
        public async Task<IActionResult> GetListSubjectImageFile()
        {
            GetListResponse<GetListSubjectImageFileListItemDto> response = await Mediator.Send(new GetListSubjectImageFileQuery());
            return Ok(response);
        }

        [HttpGet("GetByIdSubjectImageFile/{id}")]
        public async Task<IActionResult> GetByIdSubjectImageFile([FromRoute] Guid id)
        {
            GetByIdSubjectImageFileQuery query = new() { Id = id };
            GetByIdSubjectImageFileDto response = await Mediator.Send(query);
            return Ok(response);
        }
        // ------------------- SubjectImageFile ile ilgili endpointlerin sonu ---------------------
    }
}
