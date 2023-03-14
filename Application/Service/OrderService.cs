using Application.Contract.Repository;
using Application.Contract.Services;
using Application.Exceptions;
using Domain.Entity;

namespace Application.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public async Task<Order> Create(Order order)
        {
            return await _orderRepository.Create(order);
        }

        public async Task<IEnumerable<Order>> RetrieveAll()
        {
            return await _orderRepository.RetrieveAll();
        }

        public async Task<Order?> RetrieveById(int id)
        {
            return await _orderRepository.RetrieveById(id);
        }

        public async Task<Order> Update(Order order)
        {
            var orderDb = await ThrowIfNotExistElseReturnOrder(order.Id);
            orderDb.OrderItems = order.OrderItems;
            orderDb.Address = order.Address;
            orderDb.LastUpdatedDate = DateTime.UtcNow;
            var updatedOrder = await _orderRepository.Update(orderDb);
            return updatedOrder!;
        }

        public async Task<bool> DeleteAll()
        {
            return await _orderRepository.DeleteAll();
        }

        private async Task<Order> ThrowIfNotExistElseReturnOrder(int id)
        {
            var orderDb = await RetrieveById(id);
            NotFoundException.ThrowIfNull(orderDb);
            return orderDb;
        }
    }
}
