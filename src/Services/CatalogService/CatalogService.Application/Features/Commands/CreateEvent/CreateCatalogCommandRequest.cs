using CatalogService.Domain.Common;
using MediatR;

namespace CatalogService.Application.Features.Commands.CreateEvent
{
    public class CreateCatalogCommandRequest : IRequest<BaseResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }
    }
}
