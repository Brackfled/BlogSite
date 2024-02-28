using Application.Features.RefreshTokens.Queries.GetList;
using Core.Application.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokensController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            GetListResponse<GetListRefreshTokenListItemResponse> response = await Mediator.Send(new GetListRefreshTokenQuery());
            return Ok(response);
        }
    }
}
