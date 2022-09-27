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

namespace CatalogService.Application.Features.Queries.GetItemsEvent
{
    public class GetItemsQueryHandler : IRequestHandler<GetItemsQueryRequest, PaginatedItemsViewModel<CatalogItem>>
    {
        private readonly CatalogContext context;

        public GetItemsQueryHandler(CatalogContext context)
        {
            this.context = context;
        }

        public async Task<PaginatedItemsViewModel<CatalogItem>> Handle(GetItemsQueryRequest request, CancellationToken cancellationToken)
        {
            var totalItems = await context.CatalogItems
                .LongCountAsync();

            var itemsOnPage = await context.CatalogItems
                .OrderBy(c => c.Name)
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
