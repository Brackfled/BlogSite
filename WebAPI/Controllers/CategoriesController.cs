using Application.Features.Categories.Commands.Create;
using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Queries.GetList;
using Core.Application.Request;
using Core.Application.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            CreatedCategoryResponse createdCategoryResponse = await Mediator.Send(createCategoryCommand);
            return Ok(createdCategoryResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCategoryQuery query = new() { PageRequest = pageRequest };
            GetListResponse<GetListCategoryListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            DeleteCategoryCommand command = new() { Id = id };
            DeletedCategoryResponse response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
