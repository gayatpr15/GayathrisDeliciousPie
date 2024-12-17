using System.Runtime.CompilerServices;

namespace GayathrisDeliciousPie.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IShoppingCart _shoppingCart;
        private readonly GayathrisPieShopDbContext _gayathrisPieShopDbContext;
        public OrderRepository(GayathrisPieShopDbContext gayathrisPieShopDbContext, IShoppingCart shoppingCart) {
            _gayathrisPieShopDbContext = gayathrisPieShopDbContext;
            _shoppingCart = shoppingCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();
            List<ShoppingCartItem>? items = new List<ShoppingCartItem>();
            items = _shoppingCart.GetShoppingCartItems();

            order.OrderDetails = new List<OrderDetail>();

            foreach (ShoppingCartItem? item in items)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = item.Amount,
                    PieId = item.Pie.PieId,
                    Price = item.Pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }
            _gayathrisPieShopDbContext.Orders.Add(order);
            _gayathrisPieShopDbContext.SaveChanges();
        }
    }
}
