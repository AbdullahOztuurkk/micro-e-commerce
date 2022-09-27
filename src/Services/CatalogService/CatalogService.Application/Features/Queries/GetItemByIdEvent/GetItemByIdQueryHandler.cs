using CatalogService.Application.Features.Queries.GetItemsByIdEvent;
using CatalogService.Domain;
using CatalogService.Persistence.Context;
using MediatR;

namespace CatalogService.Application.Features.Queries.GetItemByIdEvent
{
    public class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQueryRequest, CatalogItem>
    {
        private readonly CatalogContext context;

        public GetItemByIdQueryHandler(CatalogContext context)
        {
            this.context = context;
        }
        public Task<CatalogItem> Handle(GetItemByIdQueryRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(context.CatalogItems.SingleOrDefault(pred => pred.Id == request.Id));
        }
    }
}
