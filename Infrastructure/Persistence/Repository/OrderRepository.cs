using Application.Contract.Repository;
using Domain.Entity;
using Domain.ValueObjects;

namespace Infrastructure.Persistence.Repository
{
    public class OrderRepository : IOrderRepository
    {
        // A list that supposed to be an in-memory data source
        private readonly List<Order> _ordersDb;
        private readonly List<OrderItem> _orderItemsDb;

        public OrderRepository()
        {
            #region MockData

            _orderItemsDb = new List<OrderItem>()
            {
                new OrderItem()
                {
                    Id = 1,
                    OrderId = 1,
                    ItemName = "Hjärtats Järn + Vitamin C 100 st",
                    Price = 55.00m,
                    ItemId = 167
                },
                new OrderItem()
                {
                    Id = 2,
                    OrderId = 2,
                    ItemName = "Ibuprofen Orifarm 400 mg filmdragerad tablett 30 st ",
                    Price = 20.00m,
                    ItemId = 856
                },
                new OrderItem()
                {
                    Id = 3,
                    OrderId = 2,
                    ItemName = "La Roche-Posay Cicaplast Baume B5+ 40ml",
                    Price = 95.00m,
                    ItemId = 345
                },
            };

            Address address = new()
            {
                City = "Stockholm",
                Country = "Sweden",
                Street = "Götgatan 78",
                ZipCode = "11830"
            };

            _ordersDb = new List<Order>()
            {
                new Order()
                {
                    Id = 1,
                    UserId = 1,
                    CreatedDate = DateTime.Now,
                    LastUpdatedDate = DateTime.Now,
                    OrderItems = new List<OrderItem>() { _orderItemsDb[0] },
                    Address = address,
                    PhoneNumber = "+46700000001",
                    Email = "user1@apotekethjartat.se",
                },
                new Order()
                {
                    Id = 2,
                    UserId = 1,
                    CreatedDate = DateTime.Now,
                    LastUpdatedDate = DateTime.Now,
                    OrderItems = new List<OrderItem>() { _orderItemsDb[1], _orderItemsDb[2] },
                    Address = address,
                    PhoneNumber = "+46700000002",
                    Email = "user2@apotekethjartat.se",
                },
            };

            #endregion
        }

        public Task<Order> Create(Order order)
        {
            order.Id = _ordersDb.Count + 1;

            var orderItems = order.OrderItems!.ToList();

            for (int i = 0; i < orderItems.Count; i++)
            {
                orderItems[i].OrderId = order.Id;
                orderItems[i].Id = _orderItemsDb.Count + 1;

                _orderItemsDb.Add(orderItems[i]);
            }

            // Overwrite (update) the list of order-items
            order.OrderItems = orderItems.AsEnumerable();

            _ordersDb.Add(order);

            return Task.FromResult(order);
        }

        public Task<bool> DeleteAll()
        {
            _ordersDb.Clear();

            return Task.FromResult(true);
        }

        public Task<IEnumerable<Order>> RetrieveAll()
        {
            return Task.FromResult(_ordersDb.AsEnumerable());
        }

        public Task<Order?> RetrieveById(int id)
        {
            return Task.FromResult(_ordersDb.SingleOrDefault(x => x.Id == id));
        }

        public Task<Order?> Update(Order order)
        {
            var index = _ordersDb.FindIndex(o => o.Id == order.Id);

            if (index != -1)
            {
                Order orderDb = _ordersDb[index];

                var orderItems = order.OrderItems!.ToList();

                for (int i = 0; i < orderItems.Count; i++)
                {
                    // Ensure relationship between order-item and order
                    orderItems[i].OrderId = order.Id;

                    var orderItemDb = _orderItemsDb.SingleOrDefault(oi => oi.ItemId == orderItems[i].ItemId);
                    if (orderItemDb != default)
                    {
                        // Ensure order-item id is valid
                        orderItems[i].Id = orderItemDb.Id;
                    }
                    else
                    {
                        // Add new order item if it does not exist
                        orderItems[i].Id = _orderItemsDb.Count + 1;
                        orderItems[i].OrderId = order.Id;

                        _orderItemsDb.Add(orderItems[i]);
                    }
                }

                _ordersDb[index] = order;
            }
            else
            {
                return Task.FromResult<Order?>(default);
            }

            return Task.FromResult(order)!;
        }
    }
}
