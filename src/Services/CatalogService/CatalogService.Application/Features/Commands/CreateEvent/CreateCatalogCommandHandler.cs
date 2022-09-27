using AutoMapper;
using CatalogService.Domain;
using CatalogService.Domain.Common;
using CatalogService.Persistence.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogService.Application.Features.Commands.CreateEvent
{
    public class CreateCatalogCommandHandler : IRequestHandler<CreateCatalogCommandRequest, BaseResponse>
    {
        private readonly CatalogContext context;
        private readonly IMapper mapper;

        public CreateCatalogCommandHandler(CatalogContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<BaseResponse> Handle(CreateCatalogCommandRequest request, CancellationToken cancellationToken)
        {
            CatalogItem catalogItem = mapper.Map<CatalogItem>(request);
            context.CatalogItems.Add(catalogItem);
            await context.SaveChangesAsync();

            return new SuccessResponse("Catalog Item has been added!");
        }
    }
}
