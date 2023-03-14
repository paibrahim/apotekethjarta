using FluentValidation;

namespace API.Models.V1
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public string? ItemName { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderItemValidator : AbstractValidator<OrderItem>
    {
        public OrderItemValidator()
        {
            _ = RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            _ = RuleFor(x => x.OrderId).NotNull().GreaterThanOrEqualTo(0);
            _ = RuleFor(x => x.ItemId).NotNull().GreaterThan(0);
            _ = RuleFor(x => x.ItemName).NotNull().Length(2, 255);
            _ = RuleFor(x => x.Price).GreaterThan(0);
            _ = RuleFor(x => x.Discount).NotNull().InclusiveBetween(0, 1);
        }
    }
}
