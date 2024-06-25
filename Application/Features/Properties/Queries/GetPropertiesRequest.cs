using Application.Models;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Properties.Queries
{
    public class GetPropertiesRequest : IRequest<List<PropertyDTO>>
    {
    }

    public class GetPropertiesRequestHandler : IRequestHandler<GetPropertiesRequest, List<PropertyDTO>>
    {
        private readonly IPropertyRepo _propertyRepo;
        private readonly IMapper _mapper;

        public GetPropertiesRequestHandler(IPropertyRepo propertyRepo, IMapper mapper)
        {
            _propertyRepo = propertyRepo;
            _mapper = mapper;
        }

        public async Task<List<PropertyDTO>> Handle(GetPropertiesRequest request, CancellationToken cancellationToken)
        {
            var propertiesInDb = await _propertyRepo.GetAllAsync();

            if (propertiesInDb != null)
            {
                var propertiesDTO = _mapper.Map<List<PropertyDTO>>(propertiesInDb);

                return propertiesDTO;
            }

            return null;
        }
    }
}
