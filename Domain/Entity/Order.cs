using Domain.Entity.Base;
using Domain.ValueObjects;

namespace Domain.Entity
{
    public class Order : BaseEntity
    {
        public int UserId { get; set; }
        public IEnumerable<OrderItem>? OrderItems { get; set; }
        public Address? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
