using CatalogService.Application.ViewModels;
using CatalogService.Domain;
using MediatR;

namespace CatalogService.Application.Features.Queries.GetItemsByNameEvent
{
    public class GetItemsByNameQueryRequest:IRequest<PaginatedItemsViewModel<CatalogItem>>
    {
        public GetItemsByNameQueryRequest(string name, PaginateSettings? paginateSettings = null)
        {
            Name = name;
            PaginateSettings = paginateSettings is not null ? paginateSettings : new PaginateSettings();
        }

        public string Name { get; private set; }
        public PaginateSettings PaginateSettings { get; private set; }
    }
}
