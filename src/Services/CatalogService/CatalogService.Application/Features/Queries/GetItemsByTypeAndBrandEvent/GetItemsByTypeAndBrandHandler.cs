using CatalogService.Application.ViewModels;
using CatalogService.Domain;
using CatalogService.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Application.Features.Queries.GetItemsByTypeAndBrandEvent
{
    public class GetItemsByTypeAndBrandHandler : IRequestHandler<GetItemsByTypeAndBrandQueryRequest, PaginatedItemsViewModel<CatalogItem>>
    {
        private readonly CatalogContext context;

        public GetItemsByTypeAndBrandHandler(CatalogContext context)
        {
            this.context = context;
        }
        public async Task<PaginatedItemsViewModel<CatalogItem>> Handle(GetItemsByTypeAndBrandQueryRequest request, CancellationToken cancellationToken)
        {
            var root = (IQueryable<CatalogItem>)context.CatalogItems;

            root = root.Where(ci => ci.CatalogTypeId == request.TypeId);

            if (request.BrandId.HasValue)
            {
                root = root.Where(ci => ci.CatalogBrandId == request.BrandId);
            }

            var totalItems = await root
                .LongCountAsync();

            var itemsOnPage = await root
                .Skip(request.PaginateSettings.PageSize * request.PaginateSettings.PageIndex)
                .Take(request.PaginateSettings.PageSize)
                .ToListAsync();

            return new PaginatedItemsViewModel<CatalogItem>(
                request.PaginateSettings.PageIndex,
                request.PaginateSettings.PageSize, 
                totalItems, itemsOnPage);
        }
    }
}
