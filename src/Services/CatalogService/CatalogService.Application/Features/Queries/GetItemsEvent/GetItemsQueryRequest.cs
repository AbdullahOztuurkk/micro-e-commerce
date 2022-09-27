using CatalogService.Application.ViewModels;
using CatalogService.Domain;
using MediatR;

namespace CatalogService.Application.Features.Queries.GetItemsEvent
{
    public class GetItemsQueryRequest : IRequest<PaginatedItemsViewModel<CatalogItem>>
    {
        public PaginateSettings PaginateSettings { get; set; }
    }
}
