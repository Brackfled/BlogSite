using Application.Features.ImageFiles.Rules;
using Application.Services.BlogFileService;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Stroage;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ImageFiles.Commands.Delete
{
    public class DeleteImageFileCommand: IRequest<DeletedImageFileResponse>
    {
        public Guid Id { get; set; }

        public class DeleteImageFileCommandHandler: IRequestHandler<DeleteImageFileCommand, DeletedImageFileResponse>
        {
            private readonly IImageFileRepository _imageFileRepository;
            private readonly IBlogFileService _blogFileService;
            private readonly IStroage _stroage;
            private readonly ImageFileBusinessRules _imageFileBusinessRules;
            private IMapper _mapper;

            public DeleteImageFileCommandHandler(IBlogFileService blogFileService, IStroage stroage, IImageFileRepository imageFileRepository, ImageFileBusinessRules imageFileBusinessRules, IMapper mapper)
            {
                _imageFileRepository = imageFileRepository;
                _imageFileBusinessRules = imageFileBusinessRules;
                _stroage = stroage;
                _blogFileService = blogFileService;
                _mapper = mapper;
            }

            public async Task<DeletedImageFileResponse> Handle(DeleteImageFileCommand request, CancellationToken cancellationToken)
            {
                await _imageFileBusinessRules.ImageFileShouldBeExistsWhenSelected(request.Id);

                ImageFile? imageFile = await _imageFileRepository.GetAsync(predicate: iff => iff.Id == request.Id, include: x => x.Include(opt => opt.BlogFile), withDeleted: false, cancellationToken: cancellationToken);

                _mapper.Map(request, imageFile);

                if (imageFile != null)
                {
                    if(imageFile.BlogFile != null)
                        await _blogFileService.DeleteAsync(imageFile.BlogFile, permanent: false);
                    await _imageFileRepository.DeleteAsync(imageFile, permanent: false);
                }

                await _stroage.DeleteAsync(imageFile.BlogFile.FilePath, imageFile.BlogFile.Name);


                DeletedImageFileResponse response = _mapper.Map<DeletedImageFileResponse>(imageFile);
                return response;

            }
        }
    }
}
