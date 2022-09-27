using CatalogService.Domain.Common;
using MediatR;

namespace CatalogService.Application.Features.Commands.UpdateEvent
{
    public class UpdateCatalogCommandRequest: IRequest<BaseResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
    }
}
