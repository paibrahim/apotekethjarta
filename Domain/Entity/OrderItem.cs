using Domain.Entity.Base;

namespace Domain.Entity
{
    public class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
    }
}
