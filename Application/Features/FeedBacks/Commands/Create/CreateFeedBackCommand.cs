using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedBacks.Commands.Create
{
    public class CreateFeedBackCommand: IRequest<CreatedFeedBackResponse>, ICacheRemoverRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }

        public string? CacheKey => "";

        public bool ByPassCache { get;}

        public string? CacheGroupKey => "GetListFeedBacks";

        public CreateFeedBackCommand(string name, string email, string text)
        {
            Name = name;
            Email = email;
            Text = text;
        }

        public class CreateFeedBackCommandHandler: IRequestHandler<CreateFeedBackCommand, CreatedFeedBackResponse> 
        {
            private readonly IFeedBackRepository _feedBackRepository;
            private IMapper _mapper;

            public CreateFeedBackCommandHandler(IFeedBackRepository feedBackRepository, IMapper mapper)
            {
                _feedBackRepository = feedBackRepository;
                _mapper = mapper;
            }

            public async Task<CreatedFeedBackResponse> Handle(CreateFeedBackCommand request, CancellationToken cancellationToken)
            {
                FeedBack feedBack = _mapper.Map<FeedBack>(request);
                feedBack.Id = Guid.NewGuid();
                FeedBack addedFeedBack = await _feedBackRepository.AddAsync(feedBack);

                CreatedFeedBackResponse response = _mapper.Map<CreatedFeedBackResponse>(addedFeedBack);
                return response;
            }
        }
    }
}
