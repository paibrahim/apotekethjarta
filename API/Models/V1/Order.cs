using FluentValidation;

namespace API.Models.V1
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Address? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public IEnumerable<OrderItem>? OrderItems { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }

    public class Address
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
    }

    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            _ = RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
            _ = RuleFor(x => x.UserId).GreaterThan(0);
            _ = RuleFor(x => x.CreatedDate).NotNull();
            _ = RuleFor(x => x.Email).NotNull().EmailAddress().Length(6, 255);
            _ = RuleFor(x => x.PhoneNumber).Length(6, 12).When(x => x.PhoneNumber != default);
            _ = RuleFor(x => x.LastUpdatedDate).NotNull();
            _ = RuleFor(x => x.Address).SetValidator(new AddressValidator());
            _ = RuleForEach(x => x.OrderItems).SetValidator(new OrderItemValidator());
        }
    }
    public class AddressValidator : AbstractValidator<Address?>
    {
        public AddressValidator()
        {
            _ = RuleFor(x => x.Street).NotNull().Length(2, 255);
            _ = RuleFor(x => x.City).NotNull().Length(2, 255);
            _ = RuleFor(x => x.ZipCode).NotNull().Length(5);
            _ = RuleFor(x => x.Country).NotNull().Length(2, 255);
        }
    }
}
