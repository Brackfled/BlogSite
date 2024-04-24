using Application.Features.FeedBacks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedBacks.Commands.Delete
{
    public class DeleteFeedBackCommand: IRequest<DeletedFeedBackResponse>, ICacheRemoverRequest, ILoggableRequest
    {
        public Guid Id { get; set; }

        public string? CacheKey => "";

        public bool ByPassCache { get; }

        public string? CacheGroupKey => "GetListFeedBacks";

        public class DeleteFeedBackCommandHandler: IRequestHandler<DeleteFeedBackCommand, DeletedFeedBackResponse>
        {
            private readonly IFeedBackRepository _feedBackRepository;
            private readonly FeedBackBusinessRules _feedBackBusinessRules;
            private IMapper _mapper;

            public DeleteFeedBackCommandHandler(IFeedBackRepository feedBackRepository, FeedBackBusinessRules feedBackBusinessRules, IMapper mapper)
            {
                _feedBackRepository = feedBackRepository;
                _feedBackBusinessRules = feedBackBusinessRules;
                _mapper = mapper;
            }

            public async Task<DeletedFeedBackResponse> Handle(DeleteFeedBackCommand request, CancellationToken cancellationToken)
            {
                await _feedBackBusinessRules.FeedBackShouldBeExistsWhenSelected(request.Id);

                FeedBack? feedBack = await _feedBackRepository.GetAsync(predicate: f => f.Id == request.Id,
                                                                        cancellationToken: cancellationToken
                                                                       );

                _mapper.Map(request, feedBack);
                FeedBack deletedFeedBack = await _feedBackRepository.DeleteAsync(feedBack);


                DeletedFeedBackResponse response = _mapper.Map<DeletedFeedBackResponse>(deletedFeedBack);
                return response;
            }
        }
    }
}
