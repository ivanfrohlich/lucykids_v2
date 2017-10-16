using Lucykids_v2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lucykids_v2.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext _storeDbContext;
        private readonly Cart _cart;


        public OrderRepository(StoreDbContext storeDbContext, Cart cart)
        {
            _storeDbContext = storeDbContext;
            _cart = cart;
        }


        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            _storeDbContext.Orders.Add(order);

            var shoppingCartItems = _cart.CartLines;
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Quantity = shoppingCartItem.Quantity,
                    ProuctId = shoppingCartItem.Product.ProductId,
                    OrderId = order.OrderId,
                    Price = shoppingCartItem.Product.Price
                };

                _storeDbContext.OrderDetails.Add(orderDetail);
            }

            _storeDbContext.SaveChanges();
        }
    }
}
