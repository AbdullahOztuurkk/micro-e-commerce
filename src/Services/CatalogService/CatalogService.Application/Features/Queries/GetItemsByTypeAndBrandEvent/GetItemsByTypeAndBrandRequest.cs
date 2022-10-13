using CatalogService.Application.ViewModels;
using CatalogService.Domain;
using MediatR;

namespace CatalogService.Application.Features.Queries.GetItemsByTypeAndBrandEvent
{
    public class GetItemsByTypeAndBrandQueryRequest : IRequest<PaginatedItemsViewModel<CatalogItem>>
    {
        public GetItemsByTypeAndBrandQueryRequest(int typeId, int? brandId, PaginateSettings? paginateSettings)
        {
            TypeId = typeId;
            BrandId = brandId; 
            PaginateSettings = paginateSettings is not null ? paginateSettings : new PaginateSettings();
        }

        public int TypeId { get; private set; }
        public int? BrandId { get; private set; }
        public PaginateSettings PaginateSettings { get; private set; }
    }
}
