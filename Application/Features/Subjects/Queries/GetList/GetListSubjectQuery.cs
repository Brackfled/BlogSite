using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Request;
using Core.Application.Response;
using Core.Persistance.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Queries.GetList
{
    public class GetListSubjectQuery : IRequest<GetListResponse<GetListSubjectListItemDto>>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListSubjectQueryHandler: IRequestHandler<GetListSubjectQuery, GetListResponse<GetListSubjectListItemDto>>
        {
            private readonly ISubjectRepository _subjectRepository;
            private IMapper _mapper;

            public GetListSubjectQueryHandler(ISubjectRepository subjectRepository, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _mapper = mapper;
            }

            public async Task<GetListResponse<GetListSubjectListItemDto>> Handle(GetListSubjectQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Subject> subjects = await _subjectRepository.GetListAsync(
                                                                                      index:request.PageRequest.PageIndex,
                                                                                      size:request.PageRequest.PageSize,
                                                                                      withDeleted:true,
                                                                                      cancellationToken:cancellationToken
                                                                                      );

                var mappedSubjectListModel = _mapper.Map<GetListResponse<GetListSubjectListItemDto>>(subjects);
                return mappedSubjectListModel;
            }
        }
    }

}
