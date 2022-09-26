using CatalogService.Application.ViewModels;
using CatalogService.Domain;
using CatalogService.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Features.Queries.GetItemsByBrandEvent
{
    internal class GetItemsByBrandQueryHandler : IRequestHandler<GetItemsByBrandQueryRequest, PaginatedItemsViewModel<CatalogItem>>
    {
        private readonly CatalogContext context;

        public GetItemsByBrandQueryHandler(CatalogContext context)
        {
            this.context = context;
        }
        public async Task<PaginatedItemsViewModel<CatalogItem>> Handle(GetItemsByBrandQueryRequest request, CancellationToken cancellationToken)
        {
            var root = (IQueryable<CatalogItem>)context.CatalogItems;

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
                totalItems,
                itemsOnPage);
        }
    }
}
