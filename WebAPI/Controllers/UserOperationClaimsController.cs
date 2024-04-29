using Application.Features.UserOperationClaims.Commands.Create;
using Application.Features.UserOperationClaims.Commands.Delete;
using Application.Features.UserOperationClaims.Commands.Update;
using Application.Features.UserOperationClaims.Queries.GetList;
using Core.Application.Request;
using Core.Application.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserOperationClaimCommand createUserOperationClaimCommand)
        {
            CreatedUserOperationClaimResponse result = await Mediator.Send(createUserOperationClaimCommand);
            return Created(uri: "", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserOperationClaimQuery getListUserOperationClaimQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListUserOperationClaimListItemDto> result = await Mediator.Send(getListUserOperationClaimQuery);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] DeleteUserOperationClaimCommand deleteUserOperationClaimCommand)
        {
            DeletedUserOperationClaimResponse result = await Mediator.Send(deleteUserOperationClaimCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserOperationClaimCommand updateUserOperationClaimCommand)
        {
            UpdatedUserOperationClaimResponse result = await Mediator.Send(updateUserOperationClaimCommand);
            return Ok(result);
        }
    }
}
