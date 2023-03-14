using Domain.Entity;

namespace Application.Contract.Services
{
    public interface IOrderService
    {
        Task<Order> Create(Order order);
        Task<bool> DeleteAll();
        Task<IEnumerable<Order>> RetrieveAll();
        Task<Order?> RetrieveById(int id);
        Task<Order> Update(Order order);
    }
}