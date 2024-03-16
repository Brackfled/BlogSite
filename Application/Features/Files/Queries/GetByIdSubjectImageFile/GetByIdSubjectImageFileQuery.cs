using Amazon.Runtime.Internal;
using Application.Features.Files.Constants;
using Application.Features.Files.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConserns.Exceptions.Types;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Files.Queries.GetByIdSubjectImageFile
{
    public class GetByIdSubjectImageFileQuery: IRequest<GetByIdSubjectImageFileDto>
    {
        public Guid Id { get; set; }

        public class GetByIdSubjectImageFileQueryHandler: IRequestHandler<GetByIdSubjectImageFileQuery, GetByIdSubjectImageFileDto>
        {
            private readonly ISubjectImageFileRepository _subjectImageFileRepository;
            private readonly FileBusinessRules _fileBusinessRules;
            private IMapper _mapper;

            public GetByIdSubjectImageFileQueryHandler(ISubjectImageFileRepository subjectImageFileRepository, FileBusinessRules fileBusinessRules, IMapper mapper)
            {
                _subjectImageFileRepository = subjectImageFileRepository;
                _fileBusinessRules = fileBusinessRules;
                _mapper = mapper;
            }

            public async Task<GetByIdSubjectImageFileDto> Handle(GetByIdSubjectImageFileQuery request, CancellationToken cancellationToken)
            {
                SubjectImageFile? subjectImageFile = await _subjectImageFileRepository.GetAsync(
                        predicate: x => x.Id == request.Id,
                        include: x=> x.Include(y => y.Subject),
                        withDeleted:true,
                        cancellationToken:cancellationToken
                    );

                if (subjectImageFile == null)
                    throw new BusinessException(FileMessages.SubjectImageFileIsNotExists);

                GetByIdSubjectImageFileDto response = _mapper.Map<GetByIdSubjectImageFileDto>( subjectImageFile );
                return response;
            }
        }
    }
}
