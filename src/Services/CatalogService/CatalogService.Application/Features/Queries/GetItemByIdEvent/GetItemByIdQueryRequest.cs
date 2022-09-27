using CatalogService.Domain;
using MediatR;

namespace CatalogService.Application.Features.Queries.GetItemsByIdEvent
{
    public class GetItemByIdQueryRequest : IRequest<CatalogItem>
    {
        public int Id { get; set; }
    }
}
