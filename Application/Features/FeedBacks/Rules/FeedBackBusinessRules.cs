using Application.Features.FeedBacks.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConserns.Exceptions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedBacks.Rules
{
    public class FeedBackBusinessRules: BaseBusinessRules
    {
        private readonly IFeedBackRepository _feedBackRepository;

        public FeedBackBusinessRules(IFeedBackRepository feedBackRepository)
        {
            _feedBackRepository = feedBackRepository;
        }

        public async Task FeedBackShouldBeExistsWhenSelected(Guid id)
        {
            bool doesExists = await _feedBackRepository.AnyAsync(x => x.Id == id);

            if (!doesExists)
                throw new BusinessException(FeedBackMessages.FeedBackNotExists);
        }
    }
}
