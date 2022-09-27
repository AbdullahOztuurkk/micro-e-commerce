using CatalogService.Domain;
using CatalogService.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Application.Features.Queries.GetCatalogTypesEvent
{
    public class GetCatalogTypesQueryRequest : IRequest<List<CatalogType>> { }
    public class GetCatalogTypesQueryHandler : IRequestHandler<GetCatalogTypesQueryRequest, List<CatalogType>>
    {
        private readonly CatalogContext context;

        public GetCatalogTypesQueryHandler(CatalogContext context)
        {
            this.context = context;
        }
        public async Task<List<CatalogType>> Handle(GetCatalogTypesQueryRequest request, CancellationToken cancellationToken)
        {
            return await context.CatalogTypes.ToListAsync();
        }
    }
}
