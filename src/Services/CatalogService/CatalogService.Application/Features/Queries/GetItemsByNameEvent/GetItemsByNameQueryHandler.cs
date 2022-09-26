using CatalogService.Application.ViewModels;
using CatalogService.Domain;
using CatalogService.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace CatalogService.Application.Features.Queries.GetItemsByNameEvent
{
    public class GetItemsByNameQueryHandler : IRequestHandler<GetItemsByNameQueryRequest, PaginatedItemsViewModel<CatalogItem>>
    {
        private readonly CatalogContext context;

        public GetItemsByNameQueryHandler(CatalogContext context)
        {
            this.context = context;
        }
        public async Task<PaginatedItemsViewModel<CatalogItem>> Handle(GetItemsByNameQueryRequest request, CancellationToken cancellationToken)
        {
            var totalItems = await context.CatalogItems
            .Where(c => c.Name.StartsWith(request.Name))
            .LongCountAsync();

            var itemsOnPage = await context.CatalogItems
                .Where(c => c.Name.StartsWith(request.Name))
                .Skip(request.PaginateSettings.PageSize * request.PaginateSettings.PageIndex)
                .Take(request.PaginateSettings.PageSize)
                .ToListAsync();

            return new PaginatedItemsViewModel<CatalogItem>(
                request.PaginateSettings.PageIndex,
                request.PaginateSettings.PageSize,
                totalItems,itemsOnPage);
        }
    }
}
