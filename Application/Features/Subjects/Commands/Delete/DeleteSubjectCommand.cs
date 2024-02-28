﻿using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Commands.Delete
{
    public class DeleteSubjectCommand: IRequest<DeletedSubjectResponse>
    {
        public Guid Id { get; set; }

        public class DeleteSubjectCommandHandler: IRequestHandler<DeleteSubjectCommand, DeletedSubjectResponse>
        {
            private readonly ISubjectRepository _subjectRepository;
            private IMapper _mapper;

            public DeleteSubjectCommandHandler(ISubjectRepository subjectRepository, IMapper mapper)
            {
                _subjectRepository = subjectRepository;
                _mapper = mapper;
            }

            public async Task<DeletedSubjectResponse> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
            {
                Subject? subject = await _subjectRepository.GetAsync(predicate: s => s.Id == request.Id);   

                _mapper.Map(request, subject);
                Subject deletedSubject = await _subjectRepository.DeleteAsync(subject);

                DeletedSubjectResponse? response = _mapper.Map<DeletedSubjectResponse>(subject);
                return response;
            }
        }
    }
}