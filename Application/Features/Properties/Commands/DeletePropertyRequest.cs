using Application.Repositories;
using MediatR;

namespace Application.Features.Properties.Commands
{
    public class DeletePropertyRequest : IRequest<bool>
    {
        public int PropertyId { get; set; }

        public DeletePropertyRequest(int propertyId)
        {
            PropertyId = propertyId;
        }
    }

    public class DeletePropertyRequestHandler : IRequestHandler<DeletePropertyRequest, bool>
    {
        private readonly IPropertyRepo _propertyRepo;

        public DeletePropertyRequestHandler(IPropertyRepo propertyRepo)
        {
            _propertyRepo = propertyRepo;
        }

        public async Task<bool> Handle(DeletePropertyRequest request, CancellationToken cancellationToken)
        {
            var propertyInDb = await _propertyRepo.GetByIdAsync(request.PropertyId);

            if (propertyInDb != null)
            {
                await _propertyRepo.DeleteAsync(propertyInDb);

                return true;
            }

            return false;
        }
    }
}
