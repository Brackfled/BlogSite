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

        [HttpGet("mailling")]
        public async Task<IActionResult> TestMail()
        {
            await _mailService.SendMailAsync(to: "a.ozdenn0@gmail.com", subject: "Örnek Mail", body: "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Mail Sunucudan Gönderilmiştir</title>\r\n    <style>\r\n        body {\r\n            font-family: Arial, sans-serif;\r\n            background-color: #f4f4f4;\r\n            margin: 0;\r\n            padding: 0;\r\n            display: flex;\r\n            justify-content: center;\r\n            align-items: center;\r\n            height: 100vh;\r\n        }\r\n        .container {\r\n            background-color: #fff;\r\n            padding: 20px;\r\n            border-radius: 10px;\r\n            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);\r\n            text-align: center;\r\n        }\r\n        .footer {\r\n            margin-top: 20px;\r\n            font-size: 12px;\r\n            color: #666;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <div class=\"container\">\r\n        <h2>Mail Sunucudan Gönderilmiştir</h2>\r\n        <p>Bu mail, özel bir uygulama tarafından gönderilmiştir.</p>\r\n        <p>Teşekkürler.</p>\r\n        <div class=\"footer\">\r\n            <p>&copy; 2024. Tüm hakları saklıdır.</p>\r\n        </div>\r\n    </div>\r\n</body>\r\n</html>", isBodyHtml:true);
            return Ok();
        }
    }
}
