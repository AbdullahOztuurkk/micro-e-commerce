using CatalogService.Application.ViewModels;
using CatalogService.Domain;
using MediatR;

namespace CatalogService.Application.Features.Queries.GetItemsByBrandEvent
{
    public class GetItemsByBrandQueryRequest : IRequest<PaginatedItemsViewModel<CatalogItem>>
    {
        public GetItemsByBrandQueryRequest(int? brandId, PaginateSettings? paginateSettings = null)
        {
            BrandId = brandId;
            PaginateSettings = paginateSettings is not null ? paginateSettings : new PaginateSettings();
        }

        public int? BrandId { get; private set; }
        public PaginateSettings PaginateSettings { get; private set; }
    }
}
