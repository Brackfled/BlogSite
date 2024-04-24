using Amazon.Runtime.Internal;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Request;
using Core.Application.Response;
using Core.Persistance.Dynamic;
using Core.Persistance.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Queries.GetListByDynamic
{
    public class GetListByDynamicSubjectQuery: IRequest<GetListResponse<GetListByDynamicSubjectListItemDto>>, ISecuredRequest
    {
        public string[] Roles => new[] { Core.Security.Constants.GeneralOperationClaims.Admin };

        public PageRequest PageRequest { get; set; }
        public DynamicQuery DynamicQuery { get; set; }


        public class GetListByDynamicSubjectQueryHandler : IRequestHandler<GetListByDynamicSubjectQuery, GetListResponse<GetListByDynamicSubjectListItemDto>>
        {
            private readonly ISubjectRepository _subjectRepository;
            private IMapper _mapper;

            public GetListByDynamicSubjectQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListByDynamicSubjectListItemDto>> Handle(GetListByDynamicSubjectQuery request, CancellationToken cancellationToken)
            {
                Paginate<Subject> subjects = await _subjectRepository.GetListByDynamicAsync(
                        dynamic: request.DynamicQuery,
                        include: s => s.Include(s => s.User).Include(s => s.Category).Include(s => s.SubjectImageFile),
                        index: request.PageRequest.PageIndex,
                        size: request.PageRequest.PageSize,
                        enableTracking: false,
                        withDeleted: true,
                        cancellationToken: cancellationToken
                    );

                GetListResponse<GetListByDynamicSubjectListItemDto> response = _mapper.Map<GetListResponse<GetListByDynamicSubjectListItemDto>>(subjects);
                return response;
            }
        }

    }
}
