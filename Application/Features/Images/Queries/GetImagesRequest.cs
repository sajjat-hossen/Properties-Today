using Application.Models;
using Application.Repositories;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Images.Queries
{
    public class GetImagesRequest : IRequest<List<ImageDTO>>
    {

    }

    public class GetImagesRequestHandler : IRequestHandler<GetImagesRequest, List<ImageDTO>>
    {
        private readonly IImageRepo _imageRepo;
        private readonly IMapper _mapper;
        public GetImagesRequestHandler(IImageRepo imageRepo, IMapper mapper)
        {
            _imageRepo = imageRepo;
            _mapper = mapper;
        }

        public async Task<List<ImageDTO>> Handle(GetImagesRequest request, CancellationToken cancellationToken)
        {
            var imagesInDb = await _imageRepo.GetAllAsync();

            if (imagesInDb != null)
            {
                var imagesDto = _mapper.Map<List<ImageDTO>>(imagesInDb);

                return imagesDto;
            }

            return null;

        }
    }
}
