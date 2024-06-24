using Application.Models;
using Application.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Properties.Queries
{
    public class GetPropertyByIdRequest : IRequest<PropertyDTO>
    {
        public int PropertyId { get; set; }

        public GetPropertyByIdRequest(int propertyId)
        {
            PropertyId = propertyId;
        }
    }

    public class GetPropertyByIdRequestHandler : IRequestHandler<GetPropertyByIdRequest, PropertyDTO>
    {
        private readonly IPropertyRepo _propertyRepo;
        private readonly IMapper _mapper;

        public GetPropertyByIdRequestHandler(IPropertyRepo propertyRepo, IMapper mapper)
        {
            _propertyRepo = propertyRepo;
            _mapper = mapper;
        }

        public async Task<PropertyDTO> Handle(GetPropertyByIdRequest request, CancellationToken cancellationToken)
        {
            var propertyInDb = await _propertyRepo.GetByIdAsync(request.PropertyId);

            if (propertyInDb != null)
            {
                var propertyDTO = _mapper.Map<PropertyDTO>(propertyInDb);

                return propertyDTO;
            }

            return null;

        }
    }
}
