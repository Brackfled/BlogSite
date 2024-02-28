using Application.Features.ImageFiles.Commands.Create;
using Application.Features.ImageFiles.Commands.Delete;
using Application.Features.ImageFiles.Queries.GetList;
using Core.Application.Request;
using Core.Application.Response;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageFilesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromQuery] string pathOrContainerName,[FromQuery] ImageFileBracket imageFileBracket, IFormFile formfile)
        {
            CreatedImageFileCommand command = new() {PathOrContainerName = pathOrContainerName, FormFile = formfile, UserId = getUserIdFromRequest()};
            CreatedImageFileResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListImageFileQuery query = new() { PageRequest = pageRequest };
            GetListResponse<GetListImageFileListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteImageFileCommand deleteImageFileCommand)
        {
            DeletedImageFileResponse response = await Mediator.Send(deleteImageFileCommand);
            return Ok(response);
        }
    }
}
