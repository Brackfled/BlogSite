using Application.Features.FeedBacks.Commands.Create;
using Application.Features.FeedBacks.Commands.Delete;
using Application.Features.FeedBacks.Queries.GetList;
using Application.Features.Subjects.Queries.GetList;
using Core.Application.Request;
using Core.Application.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBacksController :BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateFeedBackCommand createFeedBackCommand)
        {
            CreatedFeedBackResponse response = await Mediator.Send(createFeedBackCommand);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListFeedBackQuery query = new() { PageRequest = pageRequest };
            GetListResponse<GetListFeedBackListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] Guid id)
        {
            DeleteFeedBackCommand command = new() { Id = id };
            DeletedFeedBackResponse response = await Mediator.Send(command);
            return Ok(response);
        }
    }
}
