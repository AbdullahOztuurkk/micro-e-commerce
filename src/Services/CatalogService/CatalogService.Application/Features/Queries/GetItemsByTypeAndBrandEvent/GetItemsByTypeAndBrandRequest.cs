using CatalogService.Application.ViewModels;
using CatalogService.Domain;
using MediatR;

namespace CatalogService.Application.Features.Queries.GetItemsByTypeAndBrandEvent
{
    public class GetItemsByTypeAndBrandRequest : IRequest<PaginatedItemsViewModel<CatalogItem>>
    {
        public int TypeId { get; set; }
        public int? BrandId { get; set; }
        public PaginateSettings PaginateSettings { get; set; }
    }
}
