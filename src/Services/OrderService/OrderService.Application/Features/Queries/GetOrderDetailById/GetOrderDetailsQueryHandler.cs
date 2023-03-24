using AutoMapper;
using MediatR;
using OrderService.Application.Contracts.Repositories;
using OrderService.Application.Features.Queries.ViewModels;

namespace OrderService.Application.Features.Queries.GetOrderDetailById
{
    public class GetOrderDetailsQuery: IRequest<OrderDetailViewModel>
    {
        public Guid Id { get; set; }

        public GetOrderDetailsQuery(Guid orderId)
        {
            Id = orderId;
        }

    }
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailViewModel>
    {
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;

        public GetOrderDetailsQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }
        
        public async Task<OrderDetailViewModel> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var order = await orderRepository.GetByIdAsync(request.Id , o => o.OrderItems);

            var result = mapper.Map<OrderDetailViewModel>(order);

            return result;
        }
    }
}
