using Domain.Entity;

namespace Application.Contract.Repository
{
    public interface IOrderRepository
    {
        Task<Order> Create(Order order);
        Task<IEnumerable<Order>> RetrieveAll();
        Task<Order?> RetrieveById(int id);
        Task<Order?> Update(Order order);
        Task<bool> DeleteAll();
    }
}
