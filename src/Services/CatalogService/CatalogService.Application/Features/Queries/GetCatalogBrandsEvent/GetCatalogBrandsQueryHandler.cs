using CatalogService.Domain;
using CatalogService.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Application.Features.Queries.GetCatalogBrandsEvent
{
    public class GetCatalogBrandsQueryRequest : IRequest<List<CatalogBrand>> { }
    public class GetCatalogBrandsQueryHandler : IRequestHandler<GetCatalogBrandsQueryRequest, List<CatalogBrand>>
    {
        private readonly CatalogContext context;

        public GetCatalogBrandsQueryHandler(CatalogContext context)
        {
            this.context = context;
        }
        public async Task<List<CatalogBrand>> Handle(GetCatalogBrandsQueryRequest request, CancellationToken cancellationToken)
        {
            return await context.CatalogBrands.ToListAsync();
        }
    }
}
