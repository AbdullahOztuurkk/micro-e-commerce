using AutoMapper;
using CatalogService.Domain;
using CatalogService.Domain.Common;
using CatalogService.Persistence.Context;
using MediatR;

namespace CatalogService.Application.Features.Commands.UpdateEvent
{
    internal class UpdateCatalogCommandHandler : IRequestHandler<UpdateCatalogCommandRequest, BaseResponse>
    {
        private readonly CatalogContext context;
        private readonly IMapper mapper;

        public UpdateCatalogCommandHandler(CatalogContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<BaseResponse> Handle(UpdateCatalogCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.Id is <= 0)
                return new ErrorResponse("Id cannot be below 0! ");

            var catalog = mapper.Map<CatalogItem>(request);
            context.CatalogItems.Update(catalog);
            await context.SaveChangesAsync();
            
            return new SuccessResponse("Catalog has been updated successfully!");
        }
    }
}
