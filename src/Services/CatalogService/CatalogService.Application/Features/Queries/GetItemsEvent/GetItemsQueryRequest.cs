using CatalogService.Application.ViewModels;
using CatalogService.Domain;
using MediatR;

namespace CatalogService.Application.Features.Queries.GetItemsEvent
{
    public class GetItemsQueryRequest : IRequest<PaginatedItemsViewModel<CatalogItem>>
    {
        public GetItemsQueryRequest(PaginateSettings? paginateSettings = null)
        {
            PaginateSettings = paginateSettings is not null ? paginateSettings : new PaginateSettings();
        }
        public PaginateSettings PaginateSettings { get; private set; }
    }
}
