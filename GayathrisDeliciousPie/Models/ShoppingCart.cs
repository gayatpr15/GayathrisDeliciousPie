
using Microsoft.EntityFrameworkCore;

namespace GayathrisDeliciousPie.Models
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly GayathrisPieShopDbContext _gayathrisPieShopDbContext;
        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;


        public ShoppingCart(GayathrisPieShopDbContext gayathrisPieShopDbContext) {
            _gayathrisPieShopDbContext = gayathrisPieShopDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            GayathrisPieShopDbContext context = services.GetService<GayathrisPieShopDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Pie pie)
        {
            var shoppingCartItem =
                   _gayathrisPieShopDbContext.ShoppingCartItems.SingleOrDefault(
                       s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pie = pie,
                    Amount = 1
                };

                _gayathrisPieShopDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _gayathrisPieShopDbContext.SaveChanges();
        }

        public void ClearCart()
        {
            var cartItems = _gayathrisPieShopDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _gayathrisPieShopDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _gayathrisPieShopDbContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??=
                       _gayathrisPieShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Pie)
                           .ToList();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _gayathrisPieShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
               .Select(c => c.Pie.Price * c.Amount).Sum();
            return total;
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem =
                    _gayathrisPieShopDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _gayathrisPieShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _gayathrisPieShopDbContext.SaveChanges();

            return localAmount;
        }
    }
}
