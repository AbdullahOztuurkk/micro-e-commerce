using CatalogService.Application.ViewModels;
using CatalogService.Domain;
using MediatR;

namespace CatalogService.Application.Features.Queries.GetItemsByNameEvent
{
    public class GetItemsByNameQueryRequest:IRequest<PaginatedItemsViewModel<CatalogItem>>
    {
        public string Name { get; set; }
        public PaginateSettings PaginateSettings { get; set; }
    }
}
