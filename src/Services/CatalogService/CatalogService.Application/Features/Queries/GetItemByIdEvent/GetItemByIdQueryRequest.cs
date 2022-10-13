using CatalogService.Domain;
using MediatR;

namespace CatalogService.Application.Features.Queries.GetItemsByIdEvent
{
    public class GetItemByIdQueryRequest : IRequest<CatalogItem>
    {
        public GetItemByIdQueryRequest(int id)
        {
            Id = id;
        }
        public int Id { get; private set; }
    }
}
