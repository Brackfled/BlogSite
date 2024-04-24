using Application.Features.Users.Commands.Create;
using Application.Features.Users.Commands.Delete;
using Application.Features.Users.Commands.Update;
using Application.Features.Users.Commands.UpdateFromAuth;
using Application.Features.Users.Queries.GetById;
using Application.Features.Users.Queries.GetByIdDetail;
using Application.Features.Users.Queries.GetList;
using Application.Features.Users.Queries.GetListDetail;
using Core.Application.Request;
using Core.Application.Response;
using Insfrastructure.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IMailService _mailService;

        public UsersController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserCommand createUserCommad)
        {
            CreatedUserResponse response = await Mediator.Send(createUserCommad);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeletedUserResponse response = await Mediator.Send(new DeleteUserCommand { Id = id});
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUserCommand)
        {
            UpdatedUserResponse response = await Mediator.Send(updateUserCommand);
            return Ok(response);
        }

        [HttpPut("FromAuth")]
        public async Task<IActionResult> UpdateFromAuth([FromBody] UpdateUserFromAuthCommand updateUserFromAuthCommand)
        {
            updateUserFromAuthCommand.Id = getUserIdFromRequest();
            UpdatedUserFromAuthResponse response = await Mediator.Send(updateUserFromAuthCommand);
            return Ok(response);
        }

        [HttpGet("GetFromAuth")]
        public async Task<IActionResult> GetFromAuth()
        {


            GetByIdUserQuery query = new() { Id = getUserIdFromRequest() };
            GetByIdUserResponse response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserQuery getListUserQuery = new() { PageRequest = pageRequest };
            GetListResponse<GetListUserListItemDto> response = await Mediator.Send(getListUserQuery);
            return Ok(response);
        }

        [HttpGet("GetListDetail")]
        public async Task<IActionResult> GetListDetail()
        { 
            GetListResponse<GetListDetailUserListItemDto> dto = await Mediator.Send(new GetListDetailUserQuery());
            return Ok(dto);
        }

        [HttpGet("GetByIdDetail")]
        public async Task<IActionResult> GetUserOperationClaimsByUserId()
        {

            GetByIdDetailQuery query = new() { UserId = getUserIdFromRequest() };
            GetByIdDetailResponse response = await Mediator.Send(query);
            return Ok(response);

        }
    }
}
