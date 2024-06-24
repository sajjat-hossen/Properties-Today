using Application.Models;
using Application.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Images.Queries
{
    public class GetImageByIdRequest : IRequest<ImageDTO>
    {
        public int ImageId { get; set; }

        public GetImageByIdRequest(int imageId)
        {
            ImageId = imageId;
        }
    }

    public class GetImageByIdRequestHandler : IRequestHandler<GetImageByIdRequest, ImageDTO>
    {
        private readonly IImageRepo _imageRepo;
        private readonly IMapper _mapper;

        public GetImageByIdRequestHandler(IImageRepo imageRepo, IMapper mapper)
        {
            _imageRepo = imageRepo;
            _mapper = mapper;
        }

        public async Task<ImageDTO> Handle(GetImageByIdRequest request, CancellationToken cancellationToken)
        {
            var imageInDb = await _imageRepo.GetByIdAsync(request.ImageId);

            if (imageInDb != null)
            {
                var imageDto = _mapper.Map<ImageDTO>(imageInDb);

                return imageDto;
            }

            return null;
        }
    }
}
