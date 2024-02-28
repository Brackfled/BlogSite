using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Commands.Create
{
    public class CreateSubjectCommand: IRequest<CreatedSubjectResponse>
    {
        public int UserId { get; set; }
        public CreateSubjectDto CreateSubjectDto { get; set; }
        
        public class CreateSubjectCommandHandler: IRequestHandler<CreateSubjectCommand, CreatedSubjectResponse>
        {
            private readonly ISubjectRepository _subjectRepository;
            private IMapper _mapper;

            public CreateSubjectCommandHandler(ISubjectRepository subjectRepository, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _mapper = mapper;
            }

            public async Task<CreatedSubjectResponse> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
            {
                Subject subject = new() {Id = Guid.NewGuid(), CategoryId = request.CreateSubjectDto.CategoryId,
                                                              Title = request.CreateSubjectDto.Title,
                                                              Text = request.CreateSubjectDto.Text,
                                                              Summary = request.CreateSubjectDto.Summary,
                                                              UserId = request.UserId
                                                                 };
                

                await _subjectRepository.AddAsync(subject);

                CreatedSubjectResponse response = _mapper.Map<CreatedSubjectResponse>(subject);
                return response;

            }
        }
    }
}
