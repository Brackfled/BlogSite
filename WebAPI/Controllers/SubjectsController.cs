﻿using Application.Features.Subjects.Commands.Create;
using Application.Features.Subjects.Commands.CreateWithSubjectImageFile;
using Application.Features.Subjects.Commands.Delete;
using Application.Features.Subjects.Commands.Update;
using Application.Features.Subjects.Queries.GetById;
using Application.Features.Subjects.Queries.GetList;
using Application.Features.Subjects.Queries.GetListByCategoryId;
using Application.Features.Subjects.Queries.GetListByDynamic;
using Application.Features.Subjects.Queries.GetListDetails;
using Application.Features.Subjects.Queries.GetListFromAuth;
using Core.Application.Request;
using Core.Application.Response;
using Core.Persistance.Dynamic;
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

        [HttpPost("AddSubjectWithImage")]
        public async Task<IActionResult> AddWithSubjectImageFile([FromForm] CreateWithSubjectImageFileDto createWithSubjectImageFileDto,IFormFile formFile)
        {
            CreateWithSubjectImageFileCommand command = new() { UserId = getUserIdFromRequest(), CreateWithSubjectImageFileDto = createWithSubjectImageFileDto, FormFile= formFile };
            CreatedWithSubjectImageFileResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListSubjectQuery query = new() { PageRequest = pageRequest };
            GetListResponse<GetListSubjectListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            DeleteSubjectCommand command = new() { Id = id };
            DeletedSubjectResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateSubjectDto updateSubjectDto)
        {
            UpdateSubjectCommad command = new() { UpdateSubjectDto = updateSubjectDto, UserId = getUserIdFromRequest() };
            UpdatedSubjectResponse response = await Mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
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

        [HttpGet("GetListDetail")]
        public async Task<IActionResult> GetListDetail()
        {
            GetListResponse<GetListDetailSubjectListItemDto> response = await Mediator.Send(new GetListDetailSubjectQuery());
            return Ok(response);
        }

        [HttpPost("GetListByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest,[FromBody] DynamicQuery? dynamicQuery = null)
        {
            GetListByDynamicSubjectQuery query = new() { PageRequest = pageRequest, DynamicQuery = dynamicQuery};
            GetListResponse<GetListByDynamicSubjectListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("GetListByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetListByCategoryId([FromQuery] PageRequest pageRequest, [FromRoute] int categoryId)
        {
            GetListByCategoryIdSubjectQuery query = new() { PageRequest = pageRequest, CategoryId = categoryId };
            GetListResponse<GetListByCategoryIdSubjectListItemDto> response = await Mediator.Send(query);
            return Ok(response);
        }
    }
}
