using CatalogService.Domain.Common;
using MediatR;

namespace CatalogService.Application.Features.Commands.DeleteEvent
{
    public class DeleteCatalogCommandRequest : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
