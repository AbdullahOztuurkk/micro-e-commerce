using CatalogService.Domain;
using CatalogService.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Application.Features.Queries.GetCatalogTypesEvent
{
    public class GetCatalogBrandsQueryRequest : IRequest<List<CatalogType>> { }
    public class GetCatalogBrandsQueryHandler : IRequestHandler<GetCatalogBrandsQueryRequest, List<CatalogType>>
    {
        private readonly CatalogContext context;

        public GetCatalogBrandsQueryHandler(CatalogContext context)
        {
            this.context = context;
        }
        public async Task<List<CatalogType>> Handle(GetCatalogBrandsQueryRequest request, CancellationToken cancellationToken)
        {
            return await context.CatalogTypes.ToListAsync();
        }
    }
}
