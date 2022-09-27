using CatalogService.Domain.Common;
using CatalogService.Persistence.Context;
using MediatR;

namespace CatalogService.Application.Features.Commands.DeleteEvent
{
    internal class DeleteCatalogCommandHandler : IRequestHandler<DeleteCatalogCommandRequest, BaseResponse>
    {
        private readonly CatalogContext context;

        public DeleteCatalogCommandHandler(CatalogContext context)
        {
            this.context = context;
        }

        public async Task<BaseResponse> Handle(DeleteCatalogCommandRequest request, CancellationToken cancellationToken)
        {
            var catalog = context.CatalogItems.SingleOrDefault(x => x.Id == request.Id);

            if (catalog is null)
            {
                return new ErrorResponse("Catalog cannot be found!");
            }

            context.CatalogItems.Remove(catalog);

            await context.SaveChangesAsync();

            return new SuccessResponse("Catalog has been deleted successfully!");
        }
    }
}
