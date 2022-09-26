using CatalogService.Application.ViewModels;
using CatalogService.Domain;
using MediatR;

namespace CatalogService.Application.Features.Queries.GetItemsByBrandEvent
{
    public class GetItemsByBrandQueryRequest : IRequest<PaginatedItemsViewModel<CatalogItem>>
    {
        public int? BrandId { get; set; }
        public PaginateSettings PaginateSettings { get; set; }
    }
}
