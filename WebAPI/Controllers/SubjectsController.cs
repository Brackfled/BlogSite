using Application.Features.Subjects.Commands.Create;
using Application.Features.Subjects.Commands.Delete;
using Application.Features.Subjects.Commands.Update;
using Application.Features.Subjects.Queries.GetById;
using Application.Features.Subjects.Queries.GetList;
using Application.Features.Subjects.Queries.GetListFromAuth;
using Core.Application.Request;
using Core.Application.Response;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateSubjectDto createSubjectDto)
        {            
            CreateSubjectCommand command = new () { CreateSubjectDto = createSubjectDto, UserId = getUserIdFromRequest() };
            CreatedSubjectResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSubjectQuery query = new() { PageRequest = pageRequest };
            GetListResponse<GetListSubjectListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteSubjectCommand deleteSubjectCommand)
        {
            DeletedSubjectResponse response = await Mediator.Send(deleteSubjectCommand);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSubjectDto updateSubjectDto)
        {
            UpdateSubjectCommad command = new() { UpdateSubjectDto = updateSubjectDto, UserId = getUserIdFromRequest() };
            UpdatedSubjectResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            GetByIdSubjectQuery query = new() { Id = id };
            GetByIdSubjectResponse response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("GetListFromAuth")]
        public async Task<IActionResult> GetListFromAuth()
        {
            GetListFromAuthQuery query = new() { UserId = getUserIdFromRequest() };
            GetListResponse<GetListFromAuthSubjectListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
